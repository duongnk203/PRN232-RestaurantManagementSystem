import React, { useState } from 'react';
import { 
  Home, 
  ShoppingBag, 
  Users, 
  ClipboardList, 
  BarChart3, 
  Settings, 
  Bell, 
  Search,
  Plus,
  Clock,
  DollarSign,
  TrendingUp,
  User
} from 'lucide-react';

const AnJiiStaffHomepage = () => {
  const [activeTab, setActiveTab] = useState('dashboard');

  const menuItems = [
    { id: 'dashboard', icon: Home, label: 'Trang chủ' },
    { id: 'orders', icon: ShoppingBag, label: 'Đơn hàng' },
    { id: 'tables', icon: Users, label: 'Bàn ăn' },
    { id: 'menu', icon: ClipboardList, label: 'Thực đơn' },
    { id: 'reports', icon: BarChart3, label: 'Báo cáo' },
    { id: 'settings', icon: Settings, label: 'Cài đặt' }
  ];

  const quickStats = [
    { label: 'Đơn hàng hôm nay', value: '23', icon: ShoppingBag, color: 'bg-blue-500' },
    { label: 'Doanh thu', value: '2.4M VNĐ', icon: DollarSign, color: 'bg-green-500' },
    { label: 'Bàn đang phục vụ', value: '8/12', icon: Users, color: 'bg-orange-500' },
    { label: 'Thời gian trung bình', value: '25 phút', icon: Clock, color: 'bg-purple-500' }
  ];

  const recentOrders = [
    { id: '#001', table: 'Bàn 3', items: 'Phở bò, Cơm gà', status: 'Đang chuẩn bị', time: '10:30' },
    { id: '#002', table: 'Bàn 7', items: 'Bún chả, Nước mía', status: 'Hoàn thành', time: '10:25' },
    { id: '#003', table: 'Bàn 1', items: 'Bánh mì, Cà phê', status: 'Chờ xác nhận', time: '10:35' },
    { id: '#004', table: 'Bàn 5', items: 'Cơm tấm, Trà đá', status: 'Đang phục vụ', time: '10:20' }
  ];

  const getStatusColor = (status) => {
    switch(status) {
      case 'Hoàn thành': return 'bg-green-100 text-green-800';
      case 'Đang chuẩn bị': return 'bg-yellow-100 text-yellow-800';
      case 'Đang phục vụ': return 'bg-blue-100 text-blue-800';
      case 'Chờ xác nhận': return 'bg-red-100 text-red-800';
      default: return 'bg-gray-100 text-gray-800';
    }
  };

  const renderContent = () => {
    switch(activeTab) {
      case 'dashboard':
        return (
          <div className="space-y-6">
            {/* Header */}
            <div className="flex items-center justify-between">
              <div>
                <h1 className="text-2xl font-bold text-gray-900">Chào mừng trở lại!</h1>
                <p className="text-gray-600">Tổng quan hoạt động nhà hàng hôm nay</p>
              </div>
              <button className="bg-red-600 hover:bg-red-700 text-white px-4 py-2 rounded-lg flex items-center gap-2 transition-colors">
                <Plus size={20} />
                Đơn hàng mới
              </button>
            </div>

            {/* Quick Stats */}
            <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6">
              {quickStats.map((stat, index) => (
                <div key={index} className="bg-white rounded-lg shadow-md p-6 border border-gray-100">
                  <div className="flex items-center justify-between">
                    <div>
                      <p className="text-sm font-medium text-gray-600">{stat.label}</p>
                      <p className="text-2xl font-bold text-gray-900 mt-1">{stat.value}</p>
                    </div>
                    <div className={`${stat.color} p-3 rounded-lg`}>
                      <stat.icon className="w-6 h-6 text-white" />
                    </div>
                  </div>
                </div>
              ))}
            </div>

            {/* Recent Orders */}
            <div className="bg-white rounded-lg shadow-md border border-gray-100">
              <div className="p-6 border-b border-gray-100">
                <div className="flex items-center justify-between">
                  <h2 className="text-lg font-semibold text-gray-900">Đơn hàng gần đây</h2>
                  <button className="text-red-600 hover:text-red-700 text-sm font-medium">
                    Xem tất cả
                  </button>
                </div>
              </div>
              <div className="divide-y divide-gray-100">
                {recentOrders.map((order) => (
                  <div key={order.id} className="p-6 hover:bg-gray-50 transition-colors">
                    <div className="flex items-center justify-between">
                      <div className="flex-1">
                        <div className="flex items-center gap-4">
                          <span className="font-semibold text-gray-900">{order.id}</span>
                          <span className="text-gray-600">{order.table}</span>
                          <span className={`px-2 py-1 rounded-full text-xs font-medium ${getStatusColor(order.status)}`}>
                            {order.status}
                          </span>
                        </div>
                        <p className="text-gray-600 mt-1">{order.items}</p>
                      </div>
                      <div className="text-right">
                        <p className="text-sm text-gray-500">{order.time}</p>
                      </div>
                    </div>
                  </div>
                ))}
              </div>
            </div>
          </div>
        );
      case 'orders':
        return (
          <div className="space-y-6">
            <h1 className="text-2xl font-bold text-gray-900">Quản lý đơn hàng</h1>
            <div className="bg-white rounded-lg shadow-md p-8 text-center">
              <ShoppingBag className="w-16 h-16 text-gray-400 mx-auto mb-4" />
              <p className="text-gray-600">Chức năng quản lý đơn hàng đang được phát triển</p>
            </div>
          </div>
        );
      case 'tables':
        return (
          <div className="space-y-6">
            <h1 className="text-2xl font-bold text-gray-900">Quản lý bàn ăn</h1>
            <div className="bg-white rounded-lg shadow-md p-8 text-center">
              <Users className="w-16 h-16 text-gray-400 mx-auto mb-4" />
              <p className="text-gray-600">Chức năng quản lý bàn ăn đang được phát triển</p>
            </div>
          </div>
        );
      case 'menu':
        return (
          <div className="space-y-6">
            <h1 className="text-2xl font-bold text-gray-900">Quản lý thực đơn</h1>
            <div className="bg-white rounded-lg shadow-md p-8 text-center">
              <ClipboardList className="w-16 h-16 text-gray-400 mx-auto mb-4" />
              <p className="text-gray-600">Chức năng quản lý thực đơn đang được phát triển</p>
            </div>
          </div>
        );
      case 'reports':
        return (
          <div className="space-y-6">
            <h1 className="text-2xl font-bold text-gray-900">Báo cáo</h1>
            <div className="bg-white rounded-lg shadow-md p-8 text-center">
              <BarChart3 className="w-16 h-16 text-gray-400 mx-auto mb-4" />
              <p className="text-gray-600">Chức năng báo cáo đang được phát triển</p>
            </div>
          </div>
        );
      case 'settings':
        return (
          <div className="space-y-6">
            <h1 className="text-2xl font-bold text-gray-900">Cài đặt</h1>
            <div className="bg-white rounded-lg shadow-md p-8 text-center">
              <Settings className="w-16 h-16 text-gray-400 mx-auto mb-4" />
              <p className="text-gray-600">Chức năng cài đặt đang được phát triển</p>
            </div>
          </div>
        );
      default:
        return null;
    }
  };

  return (
    <div className="flex h-screen bg-gray-50">
      {/* Sidebar */}
      <div className="w-64 bg-white shadow-lg border-r border-gray-200">
        {/* Logo */}
        <div className="p-6 border-b border-gray-200">
          <div className="flex items-center gap-3">
            <div className="w-10 h-10 bg-gradient-to-br from-red-500 to-red-600 rounded-lg flex items-center justify-center">
              <span className="text-white font-bold text-lg">A</span>
            </div>
            <div>
              <h1 className="text-xl font-bold text-gray-900">AnJii</h1>
              <p className="text-sm text-gray-500">Restaurant Manager</p>
            </div>
          </div>
        </div>

        {/* Navigation */}
        <nav className="mt-6">
          {menuItems.map((item) => (
            <button
              key={item.id}
              onClick={() => setActiveTab(item.id)}
              className={`w-full flex items-center gap-3 px-6 py-3 text-left transition-colors ${
                activeTab === item.id
                  ? 'bg-red-50 text-red-700 border-r-2 border-red-700'
                  : 'text-gray-700 hover:bg-gray-50'
              }`}
            >
              <item.icon size={20} />
              <span className="font-medium">{item.label}</span>
            </button>
          ))}
        </nav>

        {/* User Profile */}
        <div className="absolute bottom-0 w-64 p-6 border-t border-gray-200 bg-white">
          <div className="flex items-center gap-3">
            <div className="w-10 h-10 bg-gray-200 rounded-full flex items-center justify-center">
              <User size={20} className="text-gray-600" />
            </div>
            <div>
              <p className="font-medium text-gray-900">Nguyễn Văn A</p>
              <p className="text-sm text-gray-500">Nhân viên phục vụ</p>
            </div>
          </div>
        </div>
      </div>

      {/* Main Content */}
      <div className="flex-1 flex flex-col">
        {/* Top Bar */}
        <div className="bg-white shadow-sm border-b border-gray-200 px-6 py-4">
          <div className="flex items-center justify-between">
            <div className="flex items-center gap-4">
              <div className="relative">
                <Search className="w-5 h-5 text-gray-400 absolute left-3 top-1/2 transform -translate-y-1/2" />
                <input
                  type="text"
                  placeholder="Tìm kiếm..."
                  className="pl-10 pr-4 py-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-red-500 focus:border-transparent"
                />
              </div>
            </div>
            <div className="flex items-center gap-4">
              <button className="relative p-2 text-gray-600 hover:bg-gray-100 rounded-lg">
                <Bell size={20} />
                <span className="absolute -top-1 -right-1 w-5 h-5 bg-red-500 text-white text-xs rounded-full flex items-center justify-center">
                  3
                </span>
              </button>
              <div className="text-right">
                <p className="text-sm font-medium text-gray-900">Ca làm việc</p>
                <p className="text-xs text-gray-500">08:00 - 16:00</p>
              </div>
            </div>
          </div>
        </div>

        {/* Content */}
        <div className="flex-1 overflow-auto p-6">
          {renderContent()}
        </div>
      </div>
    </div>
  );
};

export default AnJiiStaffHomepage;