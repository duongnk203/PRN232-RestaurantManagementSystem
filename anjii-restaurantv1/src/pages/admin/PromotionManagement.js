import React, { useState, useEffect } from "react";
import {
  Table,
  Button,
  Modal,
  Form,
  Input,
  Select,
  DatePicker,
  Space,
  message,
  Tag,
  Tooltip,
  Row,
  Col,
} from "antd";
import dayjs from "dayjs";
import { PlusOutlined, EditOutlined, DeleteOutlined } from "@ant-design/icons";
import { promotionService } from "../../services/promotionService";
import { promotionTypeService } from "../../services/promotionTypeService";

const { Option } = Select;
const { Search } = Input;

const PromotionManagement = () => {
  const [promotionForm] = Form.useForm();
  const [promotionTypes, setPromotionTypes] = useState([]);
  const [promotions, setPromotions] = useState([]);
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [editingPromotion, setEditingPromotion] = useState(null);
  const [loading, setLoading] = useState(false);
  const [filters, setFilters] = useState({
    page: 1,
    limit: 10,
    searchPromotion: null,
    isActive: null,
    promotionTypeId: null,
  });
  const [optionType2, setOptionType2] = useState(1); // default radio option

  const fetchPromotionTypes = async () => {
    try {
      const response = await promotionTypeService.getPromotionTypes();
      if (response.data && response.isSuccess) {
        setPromotionTypes(response.data);
      } else {
        message.error("Không thể tải danh sách loại khuyến mãi");
      }
    } catch (error) {
      console.error("Error fetching promotion types:", error);
      message.error("Không thể tải danh sách loại khuyến mãi");
    }
  };

  const fetchPromotions = async () => {
    try {
      setLoading(true);
      const res = await promotionService.getPromotions(filters);
      if (res.data && res.isSuccess) {
        setPromotions(res.data.promotions);
      }
    } catch (err) {
      message.error("Không thể tải danh sách khuyến mãi");
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchPromotionTypes();
    fetchPromotions();
  }, [filters]);

  const columns = [
    {
      title: "STT",
      key: "index",
      render: (_, __, index) => index + 1,
    },
    {
      title: "Tên khuyến mãi",
      dataIndex: "promotionName",
      key: "promotionName",
    },
    {
      title: "Loại khuyến mãi",
      dataIndex: "promotionTypeId",
      key: "promotionTypeId",
      filters: promotionTypes.map((type) => ({
        text: type.promotionTypeName,
        value: type.promotionTypeId,
      })),
      onFilter: (value, record) => record.promotionTypeId === value,
      render: (typeId) => {
        const type = promotionTypes.find((t) => t.promotionTypeId === typeId);
        return type ? type.promotionTypeName : "Không xác định";
      },
    },
    {
      title: "Giá trị giảm (%)",
      dataIndex: "discountValue",
      key: "discountValue",
      render: (val) => val ?? "-",
    },
    {
      title: "Giảm tối đa",
      dataIndex: "maxDiscountAmount",
      key: "maxDiscountAmount",
      render: (val) => (val ? `${val.toLocaleString()}₫` : "-"),
    },
    {
      title: "Mua / Tặng",
      key: "buyGet",
      render: (_, record) =>
        record.buyQuantity && record.getQuantity
          ? `${record.buyQuantity} / ${record.getQuantity}`
          : "-",
    },
    {
      title: "Thời gian",
      key: "timeRange",
      render: (record) =>
        `${new Date(record.startDate).toLocaleDateString()} - ${new Date(
          record.endDate
        ).toLocaleDateString()}`,
    },
    {
      title: "Trạng thái",
      dataIndex: "isActive",
      key: "isActive",
      filters: [
        { text: "Hoạt động", value: true },
        { text: "Không hoạt động", value: false },
      ],
      onFilter: (value, record) => record.isActive === value,
      render: (isActive) => (
        <Tag color={isActive ? "green" : "red"}>
          {isActive ? "Hoạt động" : "Không hoạt động"}
        </Tag>
      ),
    },
    {
      title: "Hành động",
      key: "actions",
      render: (_, record) => (
        <Space>
          <Button icon={<EditOutlined />} onClick={() => handleEdit(record)}>
            Sửa
          </Button>
          <Button
            icon={<DeleteOutlined />}
            danger
            onClick={() => handleDelete(record.promotionId)}
          >
            Xóa
          </Button>
        </Space>
      ),
    },
  ];

  const handleSearch = (value) => {
    setFilters((prev) => ({ ...prev, searchPromotion: value, page: 1 }));
  };

  const handleEdit = (promotion) => {
    console.log("Editing promotion:", promotion);
    setEditingPromotion({
      ...promotion,
      startDate: promotion.startDate ? dayjs(promotion.startDate) : null,
      endDate: promotion.endDate ? dayjs(promotion.endDate) : null,
    });
    promotionForm.setFieldsValue({
      promotionId: promotion.promotionId,
      promotionName: promotion.promotionName,
      promotionTypeId: promotion.promotionTypeId,
      description: promotion.description,
      startDate: promotion.startDate ? dayjs(promotion.startDate) : null,
      endDate: promotion.endDate ? dayjs(promotion.endDate) : null,
      isActive: promotion.isActive,
      discountValue: promotion.discountValue,
      maxDiscountAmount: promotion.maxDiscountAmount,
      buyQuantity: promotion.buyQuantity,
      getQuantity: promotion.getQuantity,
    });
    setIsModalVisible(true);
  };

  const handleDelete = async (id) => {
    try {
      const res = await promotionService.deletePromotion(id);
      if (res.isSuccess) {
        message.success("Xóa thành công");
        fetchPromotions();
      } else {
        message.error(res.message);
      }
    } catch (error) {
      console.error("Error:", error);
      message.error("Xóa thất bại");
    }
  };

  const handleSubmit = async (values) => {
    try {
      const payload = {
        promotionId: editingPromotion?.promotionId,
        promotionName: values.promotionName,
        promotionTypeId: values.promotionTypeId,
        description: values.description,
        startDate: dayjs(values.startDate).format("YYYY-MM-DDTHH:mm:ss"),
        endDate: dayjs(values.endDate).format("YYYY-MM-DDTHH:mm:ss"),
        isActive: values.isActive,
        discountValue: values.discountValue,
        maxDiscountAmount: values.maxDiscountAmount,
        buyQuantity: values.buyQuantity,
        getQuantity: values.getQuantity,
      };

      console.log("Submitting payload:", payload);

      if (editingPromotion) {
        const res = await promotionService.updatePromotion(payload);
        if (res.isSuccess) {
          message.success("Cập nhật thành công");
        } else {
          message.error(res.message);
        }
      } else {
        const { promotionId, ...createPayload } = payload;
        const res = await promotionService.createPromotion(createPayload);
        if (res.isSuccess) {
          message.success("Thêm khuyến mãi thành công");
        } else {
          message.error(res.message);
        }
      }

      setIsModalVisible(false);
      promotionForm.resetFields();
      setEditingPromotion(null);
      fetchPromotions();
    } catch (error) {
      console.error("Error:", error);
      message.error("Thao tác thất bại");
    }
  };

  return (
    <div className="p-6">
      <div className="flex justify-between items-center mb-6">
        <h1 className="text-2xl font-bold">Quản lý khuyến mãi</h1>
        <Space>
          <Search
            placeholder="Tìm theo tên khuyến mãi"
            allowClear
            onSearch={handleSearch}
            style={{ width: 300 }}
          />
          <Button
            type="primary"
            icon={<PlusOutlined />}
            onClick={() => {
              setEditingPromotion(null);
              promotionForm.resetFields();
              setIsModalVisible(true);
            }}
          >
            Thêm khuyến mãi
          </Button>
        </Space>
      </div>

      <Table
        columns={columns}
        dataSource={promotions}
        rowKey="promotionId"
        loading={loading}
        pagination={{
          current: filters.page,
          pageSize: filters.limit,
          total: promotions.length,
          onChange: (page) => setFilters((prev) => ({ ...prev, page })),
        }}
        rowClassName={() => "hoverable-row"} // add class for pointer cursor if needed
        components={{
          body: {
            row: (props) => {
              const description = props["data-row-key"]
                ? promotions.find(
                    (p) => p.promotionId === props["data-row-key"]
                  )?.description
                : null;

              return (
                <Tooltip
                  title={description || "Không có mô tả"}
                  placement="topLeft"
                  color="blue"
                >
                  <tr {...props} />
                </Tooltip>
              );
            },
          },
        }}
      />

      <Modal
        title={editingPromotion ? "Cập nhật khuyến mãi" : "Thêm khuyến mãi"}
        open={isModalVisible}
        onCancel={() => {
          setIsModalVisible(false);
          promotionForm.resetFields();
          setEditingPromotion(null);
        }}
        footer={null}
        width={800}
        destroyOnClose
      >
        <Form form={promotionForm} layout="vertical" onFinish={handleSubmit}>
          <Row gutter={16}>
            {/* Tên khuyến mãi & Loại khuyến mãi */}
            <Col span={12}>
              <Form.Item
                name="promotionName"
                label="Tên khuyến mãi"
                rules={[{ required: true, message: "Vui lòng nhập tên!" }]}
              >
                <Input />
              </Form.Item>
            </Col>

            <Col span={12}>
              <Form.Item
                name="promotionTypeId"
                label="Loại khuyến mãi"
                rules={[{ required: true, message: "Chọn loại khuyến mãi!" }]}
              >
                <Select
                  placeholder="Chọn loại khuyến mãi"
                  options={promotionTypes.map((type) => ({
                    label: type.promotionTypeName,
                    value: type.promotionTypeId,
                  }))}
                  onChange={(value) => {
                    promotionForm.setFieldsValue({
                      buyQuantity: null,
                      getQuantity: null,
                      discountValue: null,
                      maxDiscountAmount: null,
                    });
                  }}
                />
              </Form.Item>
            </Col>

            {/* Mô tả */}
            <Col span={24}>
              <Form.Item name="description" label="Mô tả">
                <Input.TextArea
                  rows={3}
                  placeholder="Nhập mô tả khuyến mãi..."
                />
              </Form.Item>
            </Col>

            {/* Discount Value & Max Discount - Only for Type 1 */}
            <Form.Item noStyle dependencies={["promotionTypeId"]}>
              {({ getFieldValue }) =>
                getFieldValue("promotionTypeId") === 1 && (
                  <>
                    <Col span={12}>
                      <Form.Item name="discountValue" label="Giá trị giảm (%)">
                        <Input type="number" placeholder="VD: 10" />
                      </Form.Item>
                    </Col>
                    <Col span={12}>
                      <Form.Item
                        name="maxDiscountAmount"
                        label="Giảm tối đa (vnđ)"
                      >
                        <Input type="number" placeholder="VD: 100000" />
                      </Form.Item>
                    </Col>
                  </>
                )
              }
            </Form.Item>

            {/* Buy / Get Quantity - Only for Type 3 */}
            <Form.Item noStyle dependencies={["promotionTypeId"]}>
              {({ getFieldValue }) =>
                getFieldValue("promotionTypeId") === 3 && (
                  <>
                    <Col span={12}>
                      <Form.Item
                        name="buyQuantity"
                        label="Số lượng mua (nếu có)"
                      >
                        <Input type="number" />
                      </Form.Item>
                    </Col>
                    <Col span={12}>
                      <Form.Item
                        name="getQuantity"
                        label="Số lượng tặng (nếu có)"
                      >
                        <Input type="number" />
                      </Form.Item>
                    </Col>
                  </>
                )
              }
            </Form.Item>

            {/* Ngày bắt đầu & kết thúc */}
            <Col span={12}>
              <Form.Item
                name="startDate"
                label="Ngày bắt đầu"
                rules={[{ required: true, message: "Chọn ngày bắt đầu!" }]}
              >
                <DatePicker
                  style={{ width: "100%" }}
                  showTime={{ format: "HH:mm" }}
                  format="YYYY-MM-DD HH:mm"
                  disabledDate={(current) =>
                    current && current < new Date().setHours(0, 0, 0, 0)
                  }
                />
              </Form.Item>
            </Col>
            <Col span={12}>
              <Form.Item
                name="endDate"
                label="Ngày kết thúc"
                rules={[{ required: true, message: "Chọn ngày kết thúc!" }]}
              >
                <DatePicker
                  style={{ width: "100%" }}
                  showTime={{ format: "HH:mm" }}
                  format="YYYY-MM-DD HH:mm"
                  disabledDate={(current) =>
                    current && current < new Date().setHours(0, 0, 0, 0)
                  }
                />
              </Form.Item>
            </Col>

            {/* Trạng thái */}
            <Col span={24}>
              <Form.Item name="isActive" label="Trạng thái">
                <Select>
                  <Option value={true}>Hoạt động</Option>
                  <Option value={false}>Không hoạt động</Option>
                </Select>
              </Form.Item>
            </Col>
          </Row>

          {/* Buttons */}
          <Form.Item className="text-right">
            <Space>
              <Button onClick={() => setIsModalVisible(false)}>Hủy</Button>
              <Button type="primary" htmlType="submit">
                {editingPromotion ? "Cập nhật" : "Thêm mới"}
              </Button>
            </Space>
          </Form.Item>
        </Form>
      </Modal>
    </div>
  );
};
export default PromotionManagement;
