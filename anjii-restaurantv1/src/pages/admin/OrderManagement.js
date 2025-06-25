import React, { useState, useEffect } from 'react';
import { Table, Card, DatePicker, Select, Tag, Space, Button, Modal, Form, Input, TimePicker, message } from 'antd';
import { EyeOutlined, UserOutlined } from '@ant-design/icons';
import moment from 'moment';
import { orderService } from '../../services/orderService';

const { RangePicker } = DatePicker;
const { Option } = Select;

const OrderManagement = () => {
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [selectedOrder, setSelectedOrder] = useState(null);
  const [isShiftModalVisible, setIsShiftModalVisible] = useState(false);
  const [form] = Form.useForm();
  const [orders, setOrders] = useState([]);
  const [shifts, setShifts] = useState([]);
  const [loading, setLoading] = useState({
    orders: false,
    shifts: false,
  });
  const [filters, setFilters] = useState({
    dateRange: [],
    status: 'all',
  });
  const [pagination, setPagination] = useState({
    orders: {
      current: 1,
      pageSize: 10,
      total: 0,
    },
    shifts: {
      current: 1,
      pageSize: 10,
      total: 0,
    },
  });

  const fetchOrders = async (page = 1, pageSize = 10) => {
    try {
      setLoading(prev => ({ ...prev, orders: true }));
      const response = await orderService.getOrders({
        page,
        limit: pageSize,
        status: filters.status !== 'all' ? filters.status : undefined,
        startDate: filters.dateRange[0]?.format('YYYY-MM-DD'),
        endDate: filters.dateRange[1]?.format('YYYY-MM-DD'),
      });
      setOrders(response.data);
      setPagination(prev => ({
        ...prev,
        orders: {
          ...prev.orders,
          total: response.total,
          current: page,
          pageSize,
        },
      }));
    } catch (error) {
      message.error('Failed to fetch orders');
    } finally {
      setLoading(prev => ({ ...prev, orders: false }));
    }
  };

  const fetchShifts = async (page = 1, pageSize = 10) => {
    try {
      setLoading(prev => ({ ...prev, shifts: true }));
      const response = await orderService.getStaffShifts({
        page,
        limit: pageSize,
        startDate: filters.dateRange[0]?.format('YYYY-MM-DD'),
        endDate: filters.dateRange[1]?.format('YYYY-MM-DD'),
      });
      setShifts(response.data);
      setPagination(prev => ({
        ...prev,
        shifts: {
          ...prev.shifts,
          total: response.total,
          current: page,
          pageSize,
        },
      }));
    } catch (error) {
      message.error('Failed to fetch shifts');
    } finally {
      setLoading(prev => ({ ...prev, shifts: false }));
    }
  };

  useEffect(() => {
    fetchOrders();
    fetchShifts();
  }, [filters]);

  const handleOrderTableChange = (pagination) => {
    fetchOrders(pagination.current, pagination.pageSize);
  };

  const handleShiftTableChange = (pagination) => {
    fetchShifts(pagination.current, pagination.pageSize);
  };

  const handleAddShift = async (values) => {
    try {
      await orderService.createStaffShift({
        ...values,
        date: values.date.format('YYYY-MM-DD'),
      });
      message.success('Shift added successfully');
      setIsShiftModalVisible(false);
      form.resetFields();
      fetchShifts(pagination.shifts.current, pagination.shifts.pageSize);
    } catch (error) {
      message.error('Failed to add shift');
    }
  };

  const orderColumns = [
    {
      title: 'Order ID',
      dataIndex: 'id',
      key: 'id',
    },
    {
      title: 'Customer',
      dataIndex: 'customer',
      key: 'customer',
    },
    {
      title: 'Staff',
      dataIndex: 'staff',
      key: 'staff',
    },
    {
      title: 'Date',
      dataIndex: 'date',
      key: 'date',
    },
    {
      title: 'Total',
      dataIndex: 'total',
      key: 'total',
      render: (total) => `$${total.toFixed(2)}`,
    },
    {
      title: 'Status',
      dataIndex: 'status',
      key: 'status',
      render: (status) => {
        const color = status === 'completed' ? 'green' : status === 'pending' ? 'gold' : 'red';
        return <Tag color={color}>{status.toUpperCase()}</Tag>;
      },
    },
    {
      title: 'Actions',
      key: 'actions',
      render: (_, record) => (
        <Button
          icon={<EyeOutlined />}
          onClick={() => {
            setSelectedOrder(record);
            setIsModalVisible(true);
          }}
        >
          View Details
        </Button>
      ),
    },
  ];

  const shiftColumns = [
    {
      title: 'Staff Name',
      dataIndex: 'name',
      key: 'name',
    },
    {
      title: 'Shift',
      dataIndex: 'shift',
      key: 'shift',
    },
    {
      title: 'Date',
      dataIndex: 'date',
      key: 'date',
    },
    {
      title: 'Orders Handled',
      dataIndex: 'ordersHandled',
      key: 'ordersHandled',
    },
    {
      title: 'Total Sales',
      dataIndex: 'totalSales',
      key: 'totalSales',
      render: (total) => `$${total.toFixed(2)}`,
    },
  ];

  return (
    <div className="p-6">
      <h1 className="text-2xl font-bold mb-6">Order & Shift Management</h1>

      <div className="mb-6 flex gap-4">
        <RangePicker
          className="w-64"
          onChange={(dates) => setFilters(prev => ({ ...prev, dateRange: dates }))}
        />
        <Select
          defaultValue="all"
          className="w-32"
          onChange={(value) => setFilters(prev => ({ ...prev, status: value }))}
        >
          <Option value="all">All Status</Option>
          <Option value="completed">Completed</Option>
          <Option value="pending">Pending</Option>
          <Option value="cancelled">Cancelled</Option>
        </Select>
      </div>

      <Card title="Orders" className="mb-6">
        <Table
          columns={orderColumns}
          dataSource={orders}
          rowKey="id"
          pagination={pagination.orders}
          loading={loading.orders}
          onChange={handleOrderTableChange}
        />
      </Card>

      <Card
        title="Staff Shifts"
        extra={
          <Button
            type="primary"
            icon={<UserOutlined />}
            onClick={() => setIsShiftModalVisible(true)}
          >
            Add Shift
          </Button>
        }
      >
        <Table
          columns={shiftColumns}
          dataSource={shifts}
          rowKey="id"
          pagination={pagination.shifts}
          loading={loading.shifts}
          onChange={handleShiftTableChange}
        />
      </Card>

      {/* Order Details Modal */}
      <Modal
        title="Order Details"
        open={isModalVisible}
        onCancel={() => setIsModalVisible(false)}
        footer={null}
      >
        {selectedOrder && (
          <div>
            <p><strong>Order ID:</strong> {selectedOrder.id}</p>
            <p><strong>Customer:</strong> {selectedOrder.customer}</p>
            <p><strong>Staff:</strong> {selectedOrder.staff}</p>
            <p><strong>Date:</strong> {selectedOrder.date}</p>
            <p><strong>Status:</strong> {selectedOrder.status}</p>
            <h3 className="mt-4 mb-2">Items:</h3>
            <Table
              columns={[
                { title: 'Item', dataIndex: 'name' },
                { title: 'Quantity', dataIndex: 'quantity' },
                { title: 'Price', dataIndex: 'price', render: (price) => `$${price.toFixed(2)}` },
              ]}
              dataSource={selectedOrder.items}
              pagination={false}
            />
            <p className="mt-4"><strong>Total:</strong> ${selectedOrder.total.toFixed(2)}</p>
          </div>
        )}
      </Modal>

      {/* Add Shift Modal */}
      <Modal
        title="Add Staff Shift"
        open={isShiftModalVisible}
        onCancel={() => {
          setIsShiftModalVisible(false);
          form.resetFields();
        }}
        footer={null}
      >
        <Form
          form={form}
          layout="vertical"
          onFinish={handleAddShift}
        >
          <Form.Item
            name="staffName"
            label="Staff Name"
            rules={[{ required: true, message: 'Please select staff!' }]}
          >
            <Select>
              <Option value="jane">Jane Smith</Option>
              <Option value="john">John Doe</Option>
            </Select>
          </Form.Item>
          <Form.Item
            name="shift"
            label="Shift"
            rules={[{ required: true, message: 'Please select shift!' }]}
          >
            <Select>
              <Option value="morning">Morning (6:00 AM - 2:00 PM)</Option>
              <Option value="afternoon">Afternoon (2:00 PM - 10:00 PM)</Option>
              <Option value="night">Night (10:00 PM - 6:00 AM)</Option>
            </Select>
          </Form.Item>
          <Form.Item
            name="date"
            label="Date"
            rules={[{ required: true, message: 'Please select date!' }]}
          >
            <DatePicker className="w-full" />
          </Form.Item>
          <Form.Item className="flex justify-end">
            <Space>
              <Button onClick={() => {
                setIsShiftModalVisible(false);
                form.resetFields();
              }}>
                Cancel
              </Button>
              <Button type="primary" htmlType="submit">
                Add Shift
              </Button>
            </Space>
          </Form.Item>
        </Form>
      </Modal>
    </div>
  );
};

export default OrderManagement; 