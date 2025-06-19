import React, { useState, useEffect } from "react";
import {
  Table,
  Button,
  Modal,
  Form,
  Input,
  Select,
  Space,
  message,
  Tag,
} from "antd";
import {
  EditOutlined,
  DeleteOutlined,
  UserAddOutlined,
} from "@ant-design/icons";
import { userService } from "../../services/staffService";
import { roleService } from "../../services/roleService";

const { Option } = Select;
const { Search } = Input;

const StaffManagement = () => {
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [form] = Form.useForm();
  const [editingStaff, setEditingStaff] = useState(null);
  const [staffList, setStaffList] = useState([]);
  const [loading, setLoading] = useState(false);
  const [filters, setFilters] = useState({
    page: 1,
    limit: 12,
    fullName: null,
    isActive: null,
    roleId: null,
  });
  const [roles, setRoles] = useState([]);

  const fetchRoles = async () => {
    const response = await roleService.getRoles();
    if (response.data && response.isSuccess) {
      setRoles(response.data);
    } else {
      message.error("Không thể tải danh sách chức vụ");
    }
  };

  const fetchStaffList = async () => {
    try {
      setLoading(true);
      const response = await userService.getUsers(filters);
      if (response.data && response.isSuccess) {
        setStaffList(response.data.staffs);
      }
    } catch (error) {
      message.error("Failed to fetch staff list");
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchStaffList();
    fetchRoles();
  }, [filters]);

  const columns = [
    {
      title: "Họ và tên",
      dataIndex: "fullName",
      key: "fullName",
      sorter: (a, b) => a.fullName.localeCompare(b.fullName),
    },
    {
      title: "Số điện thoại",
      dataIndex: "phoneNumber",
      key: "phoneNumber",
    },
    {
      title: "Email",
      dataIndex: "email",
      key: "email",
    },
    {
      title: "Chức vụ",
      dataIndex: "roleId",
      key: "roleId",
      filters: roles.map((role) => ({
        text: role.name,
        value: role.id,
      })),
      onFilter: (value, record) => record.roleId === value,
      render: (roleId) => {
        const role = roles.find((r) => r.id === roleId);
        return role ? role.name : roleId;
      },
    },
    {
      title: "Trạng thái",
      dataIndex: "isActive",
      key: "isActive",
      render: (isActive) => (
        <Tag color={isActive ? "green" : "red"}>
          {isActive ? "Đang làm việc" : "Đã nghỉ việc"}
        </Tag>
      ),
      filters: [
        { text: "Đang làm việc", value: true },
        { text: "Đã nghỉ việc", value: false },
      ],
      onFilter: (value, record) => record.isActive === value,
    },
    {
      title: "Thao tác",
      key: "actions",
      render: (_, record) => (
        <Space>
          <Button
            type="primary"
            icon={<EditOutlined />}
            onClick={() => handleEdit(record)}
          >
            Sửa
          </Button>
          <Button
            danger
            icon={<DeleteOutlined />}
            onClick={() => handleDelete(record.staffId)}
          >
            Xóa
          </Button>
        </Space>
      ),
    },
  ];

  const handleEdit = (staff) => {
    setEditingStaff(staff);
    form.setFieldsValue(staff);
    setIsModalVisible(true);
  };

  const handleDelete = async (staffId) => {
    try {
      await userService.deleteUser(staffId);
      message.success("Xóa nhân viên thành công");
      fetchStaffList();
    } catch (error) {
      message.error("Xóa nhân viên thất bại");
    }
  };

  const handleSubmit = async (values) => {
    try {
      const payload = {
        ...values,
        staffId: editingStaff.staffId,
      };

      if (editingStaff) {
        await userService.updateUser(payload);
        message.success("Cập nhật thông tin thành công");
      } else {
        await userService.createUser(values);
        message.success("Thêm nhân viên mới thành công");
      }
      setIsModalVisible(false);
      form.resetFields();
      setEditingStaff(null);
      fetchStaffList();
    } catch (error) {
      message.error("Thao tác thất bại");
    }
  };

  const handleSearch = (value) => {
    setFilters((prev) => ({
      ...prev,
      fullName: value || null,
      page: 1,
    }));
  };

  return (
    <div className="p-6">
      <div className="flex justify-between items-center mb-6">
        <h1 className="text-2xl font-bold">Quản lý nhân viên</h1>
        <Space>
          <Search
            placeholder="Tìm kiếm theo tên"
            allowClear
            onSearch={handleSearch}
            style={{ width: 300 }}
          />
          <Button
            type="primary"
            icon={<UserAddOutlined />}
            onClick={() => {
              setEditingStaff(null);
              form.resetFields();
              setIsModalVisible(true);
            }}
          >
            Thêm nhân viên
          </Button>
        </Space>
      </div>

      <Table
        columns={columns}
        dataSource={staffList}
        rowKey="email"
        loading={loading}
        pagination={{
          total: staffList.length,
          pageSize: filters.limit,
          current: filters.page,
          onChange: (page) => setFilters((prev) => ({ ...prev, page })),
        }}
      />

      <Modal
        title={editingStaff ? "Sửa thông tin nhân viên" : "Thêm nhân viên mới"}
        open={isModalVisible}
        onCancel={() => setIsModalVisible(false)}
        footer={null}
      >
        <Form form={form} layout="vertical" onFinish={handleSubmit}>
          <Form.Item
            name="fullName"
            label="Họ và tên"
            rules={[{ required: true, message: "Vui lòng nhập họ tên!" }]}
          >
            <Input />
          </Form.Item>
          <Form.Item
            name="phoneNumber"
            label="Số điện thoại"
            rules={[
              { required: true, message: "Vui lòng nhập số điện thoại!" },
              {
                pattern: /^[0-9]{10}$/,
                message: "Số điện thoại không hợp lệ!",
              },
            ]}
          >
            <Input />
          </Form.Item>
          <Form.Item
            name="email"
            label="Email"
            rules={[
              { required: true, message: "Vui lòng nhập email!" },
              { type: "email", message: "Email không hợp lệ!" },
            ]}
          >
            {/* <Input disabled={!!editingStaff} /> */}
            <Input />
          </Form.Item>
          <Form.Item
            name="roleId"
            label="Chức vụ"
            rules={[{ required: true, message: "Vui lòng chọn chức vụ!" }]}
          >
            <Select
              options={roles.map((role) => ({
                label: role.name,
                value: role.id,
              }))}
            />
          </Form.Item>
          <Form.Item
            name="isActive"
            label="Trạng thái"
            rules={[{ required: true, message: "Vui lòng chọn trạng thái!" }]}
          >
            <Select placeholder="Chọn trạng thái">
              <Option value={true}>Đang làm việc</Option>
              <Option value={false}>Đã nghỉ việc</Option>
            </Select>
          </Form.Item>
          <Form.Item className="flex justify-end">
            <Space>
              <Button onClick={() => setIsModalVisible(false)}>Hủy</Button>
              <Button type="primary" htmlType="submit">
                {editingStaff ? "Cập nhật" : "Thêm mới"}
              </Button>
            </Space>
          </Form.Item>
        </Form>
      </Modal>
    </div>
  );
};

export default StaffManagement;
