import React, { useState, useEffect } from "react";
import {
  Table,
  Button,
  Input,
  Space,
  Card,
  Select,
  Pagination,
  Switch,
  message,
  Modal,
  Form,
  Upload,
  Popconfirm,
} from "antd";
import { UploadOutlined, SearchOutlined } from "@ant-design/icons";
import { menuService } from "../../services/menuService";
import { categoryService } from "../../services/categoryService";
import { cloudinaryUpload } from "../../services/cloudinaryService";
import { InputNumber } from "antd";

const { Option } = Select;
const { Search } = Input;

const MenuManagementLayout = () => {
  const [selectedItem, setSelectedItem] = useState(null);
  const [searchItem, setSearchItem] = useState("");
  const [selectedCategory, setSelectedCategory] = useState("");
  const [isAvailable, setIsAvailable] = useState(null);
  const [menuItems, setMenuItems] = useState([]);
  const [menuItemId, setMenuItemId] = useState(null);
  const [pagination, setPagination] = useState({
    current: 1,
    pageSize: 5,
    total: 0,
  });
  const [categories, setCategories] = useState([]);
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [isEditMode, setIsEditMode] = useState(false);
  const [form] = Form.useForm();
  const [previewImage, setPreviewImage] = useState(null);
  const [fileList, setFileList] = useState([]);

  const handleSearch = () => {
    console.log("Performing search with:", searchItem);
    fetchMenuItems(1, pagination.pageSize);
  };

  const fetchMenuItems = async (page = 1, pageSize = 5) => {
    try {
      const filters = {
        page,
        limit: pageSize,
        searchItem: searchItem ? searchItem.trim() : undefined,
        categoryId: selectedCategory || undefined,
        isAvailable: isAvailable, // Send the value directly, API should handle null case
      };

      console.log("Fetching with filters:", filters); // For debugging
      const response = await menuService.getMenuItems(filters);

      if (response.isSuccess && response.data) {
        const mappedMenus = response.data.menus.map((item) => {
          const matchedCategory = categories.find(
            (cat) => cat.categoryId === item.categoryId
          );
          return {
            ...item,
            categoryName: matchedCategory
              ? matchedCategory.categoryName
              : "Unknown",
          };
        });
        setMenuItems(mappedMenus);
        setPagination((prev) => ({
          ...prev,
          total: response.data.total || 0,
          current: page,
        }));
        if (mappedMenus.length > 0 && !selectedItem) {
          setSelectedItem(mappedMenus[0]);
        }
      } else {
        message.error(response.message || "Failed to fetch menu items");
      }
    } catch (error) {
      console.error("Error fetching menu items:", error);
      message.error("Failed to fetch menu items");
    }
  };

  const fetchCategories = async () => {
    try {
      const response = await categoryService.getCategories();
      if (response?.data) {
        setCategories(response.data);
      }
    } catch (error) {
      message.error("Failed to fetch categories");
    }
  };

  useEffect(() => {
    fetchCategories();
  }, []);

  useEffect(() => {
    if (categories.length > 0) {
      fetchMenuItems();
    }
  }, [selectedCategory, isAvailable, categories]);

  const columns = [
    {
      title: "Image",
      dataIndex: "imageUrl",
      key: "imageUrl",
      render: (url) => (
        <img src={url} alt="menu" className="w-16 h-16 object-cover rounded" />
      ),
    },
    {
      title: "Name",
      dataIndex: "itemName",
      key: "itemName",
      sorter: (a, b) => a.itemName.localeCompare(b.itemName),
    },
    {
      title: "Category",
      dataIndex: "categoryName",
      key: "categoryName",
      sorter: (a, b) => a.categoryName.localeCompare(b.categoryName),
    },
    {
      title: "Price",
      dataIndex: "price",
      key: "price",
      sorter: (a, b) => a.price - b.price,
    },
    {
      title: "Cost",
      dataIndex: "cost",
      key: "cost",
      sorter: (a, b) => a.cost - b.cost,
    },
    {
      title: "Available",
      dataIndex: "isAvailable",
      key: "isAvailable",
      render: (value) => (value ? "Yes" : "No"),
    },
    {
      title: "Action",
      key: "action",
      render: (_, record) => (
        <Space>
          <Button type="link" onClick={() => setSelectedItem(record)}>
            View
          </Button>
          <Button
            type="link"
            onClick={() => handleEdit(record, record.menuItemId)}
          >
            Edit
          </Button>
          <Popconfirm
            title="Sure to delete?"
            onConfirm={() => handleDelete(record.menuItemId)}
          >
            <Button type="link" danger>
              Delete
            </Button>
          </Popconfirm>
        </Space>
      ),
    },
  ];

  const showAddModal = () => {
    form.resetFields();
    setPreviewImage(null);
    setFileList([]);
    setIsEditMode(false);
    setIsModalVisible(true);
  };

  const handleEdit = (item, menuItemId) => {
    setIsEditMode(true);
    setIsModalVisible(true);
    form.setFieldsValue(item);
    setPreviewImage(item.imageUrl);
    setFileList([]);
    setMenuItemId(menuItemId);
  };

  const handleAddOrEdit = () => {
    form.validateFields().then(async (values) => {
      try {
        const payload = {
          ...values,
          menuItemId: menuItemId,
        };
        console.log("Form values before image upload:", payload);
        if (fileList.length > 0) {
          const imageUrl = await cloudinaryUpload(fileList[0]);
          console.log("Cloudinary returned URL:", imageUrl);
          values.imageUrl = imageUrl;
          console.log("Form values after adding image URL:", payload);
        }
        if (isEditMode) {
          console.log("Updating menu item with values:", payload);
          await menuService.updateMenuItem(payload);
          message.success("Menu item updated");
        } else {
          console.log("Adding new menu item with values:", payload);
          await menuService.createMenuItem(values);
          message.success("Menu item added");
        }
        setIsModalVisible(false);
        setPreviewImage(null);
        setFileList([]);
        fetchMenuItems();
      } catch (error) {
        console.error("Error details:", error);
        message.error("Failed to save menu item");
      }
    });
  };

  const handleDelete = async (id) => {
    try {
      await menuService.deleteMenuItem(id);
      message.success("Deleted successfully");
      fetchMenuItems();
    } catch {
      message.error("Delete failed");
    }
  };

  return (
    <div className="p-6">
      <h1 className="text-2xl font-bold mb-4">Menu Management</h1>

      <div className="flex gap-4 mb-4">
        <Search
          placeholder="Search menu"
          className="w-64"
          value={searchItem}
          onChange={(e) => setSearchItem(e.target.value)}
          onSearch={handleSearch}
          enterButton={<SearchOutlined />}
          allowClear
        />
        <Select
          allowClear
          placeholder="Filter by category"
          className="w-64"
          value={selectedCategory}
          onChange={(value) => {
            setSelectedCategory(value);
            fetchMenuItems(1, pagination.pageSize);
          }}
        >
          {categories.map((cat) => (
            <Option key={cat.categoryId} value={cat.categoryId}>
              {cat.categoryName}
            </Option>
          ))}
        </Select>
        <div className="flex items-center gap-2">
          <span className="text-gray-600">Status:</span>
          <Select
            value={
              isAvailable === null
                ? "all"
                : isAvailable === true
                ? "available"
                : "unavailable"
            }
            onChange={(value) => {
              let newIsAvailable = null;
              if (value === "available") newIsAvailable = true;
              else if (value === "unavailable") newIsAvailable = false;

              setIsAvailable(newIsAvailable);
            }}
            onBlur={() => fetchMenuItems(1, pagination.pageSize)} // Tự fetch khi blur
            style={{ width: 160 }}
          >
            <Option value="all">Tất cả</Option>
            <Option value="available">Chỉ món có sẵn</Option>
            <Option value="unavailable">Chỉ món không có sẵn</Option>
          </Select>
        </div>
        <Button type="primary" onClick={showAddModal}>
          Add Menu
        </Button>
      </div>

      <div className="flex gap-6">
        <div className="w-1/3">
          {selectedItem && (
            <Card
              cover={
                <img
                  src={selectedItem.imageUrl}
                  alt="menu"
                  className="object-cover"
                />
              }
              variant="bordered"
            >
              <p>
                <strong>Name:</strong> {selectedItem.itemName}
              </p>
              <p>
                <strong>Category:</strong> {selectedItem.categoryName}
              </p>
              <p>
                <strong>Description:</strong> {selectedItem.description}
              </p>
              <p>
                <strong>Price:</strong> {selectedItem.price.toLocaleString()}đ
              </p>
              <p>
                <strong>Cost:</strong> {selectedItem.cost.toLocaleString()}đ
              </p>
              <p>
                <strong>Profit:</strong>{" "}
                {(selectedItem.price - selectedItem.cost).toLocaleString()}đ
              </p>
            </Card>
          )}
        </div>

        <div className="w-2/3">
          <Table
            dataSource={menuItems}
            columns={columns}
            rowKey="menuItemId"
            pagination={false}
          />
          <Pagination
            className="mt-4 text-right"
            current={pagination.current}
            pageSize={pagination.pageSize}
            total={pagination.total}
            onChange={(page, pageSize) => fetchMenuItems(page, pageSize)}
          />
        </div>
      </div>

      <Modal
        title={isEditMode ? "Chỉnh sửa món ăn" : "Thêm món ăn"}
        open={isModalVisible}
        onCancel={() => {
          setIsModalVisible(false);
          setPreviewImage(null);
          setFileList([]);
          form.resetFields();
        }}
        onOk={handleAddOrEdit}
        okText="Lưu"
        width={800}
      >
        <div className="flex gap-6">
          <div className="w-1/2">
            {previewImage && (
              <img
                src={previewImage}
                alt="preview"
                className="w-full h-64 object-cover mb-2"
              />
            )}
            <Form form={form}>
              <Form.Item name="imageUrl">
                <Upload
                  listType="picture"
                  maxCount={1}
                  fileList={fileList}
                  onRemove={() => {
                    setPreviewImage(null);
                    setFileList([]);
                  }}
                  beforeUpload={(file) => {
                    setPreviewImage(null);
                    const reader = new FileReader();
                    reader.onload = (e) => setPreviewImage(e.target.result);
                    reader.readAsDataURL(file);
                    setFileList([file]);
                    return false;
                  }}
                >
                  <Button icon={<UploadOutlined />}>Upload ảnh</Button>
                </Upload>
              </Form.Item>
            </Form>
          </div>
          <div className="w-1/2">
            <Form form={form} layout="vertical" className="pr-4">
              <Form.Item
                name="itemName"
                label="Tên món"
                rules={[{ required: true }]}
              >
                <Input />
              </Form.Item>
              <Form.Item name="description" label="Mô tả">
                <Input.TextArea />
              </Form.Item>
              <Form.Item
                name="price"
                label="Giá"
                rules={[{ required: true, type: "number", min: 0 }]}
              >
                <InputNumber style={{ width: "100%" }} />
              </Form.Item>

              <Form.Item
                name="cost"
                label="Chi phí"
                rules={[{ required: true, type: "number", min: 0 }]}
              >
                <InputNumber style={{ width: "100%" }} />
              </Form.Item>

              <Form.Item
                name="categoryId"
                label="Loại món"
                rules={[{ required: true }]}
              >
                <Select>
                  {categories.map((cat) => (
                    <Option key={cat.categoryId} value={cat.categoryId}>
                      {cat.categoryName}
                    </Option>
                  ))}
                </Select>
              </Form.Item>
              <Form.Item name="isAvailable" label="Trạng thái">
                <Select>
                  <Option value={true}>Có sẵn</Option>
                  <Option value={false}>Không có sẵn</Option>
                </Select>
              </Form.Item>
            </Form>
          </div>
        </div>
      </Modal>
    </div>
  );
};

export default MenuManagementLayout;
