import React, { useState, useEffect } from 'react';
import { Card, Row, Col, Statistic, Spin } from 'antd';
import { UserOutlined, ShoppingCartOutlined, DollarOutlined, FileOutlined } from '@ant-design/icons';
import { userService } from '../../services/staffService';
import { orderService } from '../../services/orderService';
import { menuService } from '../../services/menuService';
import { revenueService } from '../../services/revenueService';

const Dashboard = () => {
  const [loading, setLoading] = useState(true);
  const [stats, setStats] = useState({
    totalUsers: 0,
    totalOrders: 0,
    totalRevenue: 0,
    totalMenuItems: 0,
  });

  const fetchDashboardStats = async () => {
    try {
      setLoading(true);
      const [users, orders, menuItems, revenue] = await Promise.all([
        userService.getUsers(1, 1), // Just to get total count
        orderService.getOrders({ page: 1, limit: 1 }), // Just to get total count
        menuService.getMenuItems(1, 1), // Just to get total count
        revenueService.getRevenueStats(), // Get total revenue
      ]);

      setStats({
        totalUsers: users.total || 0,
        totalOrders: orders.total || 0,
        totalRevenue: revenue.totalRevenue || 0,
        totalMenuItems: menuItems.total || 0,
      });
    } catch (error) {
      console.error('Failed to fetch dashboard stats:', error);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchDashboardStats();
  }, []);

  return (
    <div className="p-6">
      <h1 className="text-2xl font-bold mb-6">Dashboard</h1>
      <Spin spinning={loading}>
        <Row gutter={16}>
          <Col span={6}>
            <Card>
              <Statistic
                title="Total Users"
                value={stats.totalUsers}
                prefix={<UserOutlined />}
              />
            </Card>
          </Col>
          <Col span={6}>
            <Card>
              <Statistic
                title="Total Orders"
                value={stats.totalOrders}
                prefix={<ShoppingCartOutlined />}
              />
            </Card>
          </Col>
          <Col span={6}>
            <Card>
              <Statistic
                title="Total Revenue"
                value={stats.totalRevenue}
                prefix={<DollarOutlined />}
                suffix="$"
                precision={2}
              />
            </Card>
          </Col>
          <Col span={6}>
            <Card>
              <Statistic
                title="Menu Items"
                value={stats.totalMenuItems}
                prefix={<FileOutlined />}
              />
            </Card>
          </Col>
        </Row>
      </Spin>
    </div>
  );
};

export default Dashboard; 