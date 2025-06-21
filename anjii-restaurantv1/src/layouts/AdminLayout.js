import React, { useState } from "react";
import { Layout, Menu } from "antd";
import {
  MenuUnfoldOutlined,
  MenuFoldOutlined,
  DashboardOutlined,
  UserOutlined,
  ShopOutlined,
  BarChartOutlined,
  ShoppingCartOutlined,
  TagOutlined,
} from "@ant-design/icons";
import { Link, useLocation } from "react-router-dom";

const { Header, Sider, Content } = Layout;

const AdminLayout = ({ children }) => {
  const [collapsed, setCollapsed] = useState(false);
  const location = useLocation();

  const menuItems = [
    {
      key: "/admin",
      icon: <DashboardOutlined />,
      label: <Link to="/admin">Dashboard</Link>,
    },
    {
      key: "/admin/users",
      icon: <UserOutlined />,
      label: <Link to="/admin/users">User Management</Link>,
    },
    {
      key: "/admin/menu",
      icon: <ShopOutlined />,
      label: <Link to="/admin/menu">Menu Management</Link>,
    },
    {
      key: "/admin/orders",
      icon: <ShoppingCartOutlined />,
      label: <Link to="/admin/orders">Order Management</Link>,
    },
    {
      key: "/admin/revenue",
      icon: <BarChartOutlined />,
      label: <Link to="/admin/revenue">Revenue Analytics</Link>,
    },
    {
      key: "/admin/promotion",
      icon: <TagOutlined />,
      label: <Link to="/admin/promotion">Promotion Management</Link>,
    },
  ];

  return (
    <Layout style={{ minHeight: "100vh" }}>
      <Sider trigger={null} collapsible collapsed={collapsed}>
        <div className="p-4">
          <h1 className="text-white text-xl font-bold">Admin Panel</h1>
        </div>
        <Menu
          theme="dark"
          mode="inline"
          selectedKeys={[location.pathname]}
          items={menuItems}
        />
      </Sider>
      <Layout>
        <Header className="bg-white p-0 flex justify-between items-center">
          <div className="p-4">
            {collapsed ? (
              <MenuUnfoldOutlined
                className="text-xl cursor-pointer"
                onClick={() => setCollapsed(false)}
              />
            ) : (
              <MenuFoldOutlined
                className="text-xl cursor-pointer"
                onClick={() => setCollapsed(true)}
              />
            )}
          </div>
          <div className="p-4">
            <span>Welcome, Admin</span>
          </div>
        </Header>
        <Content className="m-6 p-6 bg-white">{children}</Content>
      </Layout>
    </Layout>
  );
};

export default AdminLayout;
