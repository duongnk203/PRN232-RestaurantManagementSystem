import React, { useState } from 'react';
import { 
  Search, 
  Bell, 
  User, 
  Plus, 
  Minus, 
  ShoppingCart, 
  Filter,
  Star,
  Clock,
  ChefHat
} from 'lucide-react';

const OrderPage = () => {
  const [selectedCategory, setSelectedCategory] = useState('all');
  const [cart, setCart] = useState([]);
  const [searchTerm, setSearchTerm] = useState('');

  // Danh mục món ăn
  const categories = [
    { id: 'all', name: 'Tất cả', icon: ChefHat },
    { id: 'appetizer', name: 'Khai vị', icon: Star },
    { id: 'main', name: 'Món chính', icon: ChefHat },
    { id: 'dessert', name: 'Tráng miệng', icon: Star },
    { id: 'drink', name: 'Đồ uống', icon: Clock }
  ];

  // Dữ liệu menu
  const menuItems = [
    {
      id: 1,
      name: 'Phở Bò Tái',
      category: 'main',
      price: 85000,
      image: 'https://images.unsplash.com/photo-1569562211093-4ed0d0758f12?w=300&h=200&fit=crop',
      description: 'Phở bò tái truyền thống với nước dùng đậm đà',
      rating: 4.8,
      prepTime: '15 phút',
      popular: true
    },
    {
      id: 2,
      name: 'Bún Bò Huế',
      category: 'main',
      price: 75000,
      image: 'https://images.unsplash.com/photo-1559847844-d721426d6edc?w=300&h=200&fit=crop',
      description: 'Bún bò Huế cay nồng đặc trưng miền Trung',
      rating: 4.6,
      prepTime: '20 phút',
      popular: false
    },
    {
      id: 3,
      name: 'Gỏi Cuốn Tôm Thịt',
      category: 'appetizer',
      price: 45000,
      image: 'https://images.unsplash.com/photo-1588166524941-3bf61a9c41db?w=300&h=200&fit=crop',
      description: 'Gỏi cuốn tươi ngon với tôm và thịt heo',
      rating: 4.7,
      prepTime: '10 phút',
      popular: true
    },
    {
      id: 4,
      name: 'Chả Cá Lá Vọng',
      category: 'main',
      price: 120000,
      image: 'https://images.unsplash.com/photo-1565299624946-b28f40a0ca4b?w=300&h=200&fit=crop',
      description: 'Chả cá truyền thống Hà Nội với lá vọng',
      rating: 4.9,
      prepTime: '25 phút',
      popular: true
    },
    {
      id: 5,
      name: 'Chè Ba Màu',
      category: 'dessert',
      price: 35000,
      image: 'https://images.unsplash.com/photo-1551024601-bec78aea704b?w=300&h=200&fit=crop',
      description: 'Chè ba màu truyền thống mát lạnh',
      rating: 4.5,
      prepTime: '5 phút',
      popular: false
    },
    {
      id: 6,
      name: 'Cà Phê Sữa Đá',
      category: 'drink',
      price: 25000,
      image: 'https://images.unsplash.com/photo-1509042239860-f550ce710b93?w=300&h=200&fit=crop',
      description: 'Cà phê sữa đá đậm đà truyền thống',
      rating: 4.8,
      prepTime: '5 phút',
      popular: true
    },
    {
      id: 7,
      name: 'Bánh Mì Thịt Nướng',
      category: 'main',
      price: 35000,
      image: 'https://images.unsplash.com/photo-1576618148400-f54bed99fcfd?w=300&h=200&fit=crop',
      description: 'Bánh mì với thịt nướng thơm ngon',
      rating: 4.6,
      prepTime: '10 phút',
      popular: false
    },
    {
      id: 8,
      name: 'Nước Chanh Dây',
      category: 'drink',
      price: 30000,
      image: 'https://images.unsplash.com/photo-1546171753-97d7676e4602?w=300&h=200&fit=crop',
      description: 'Nước chanh dây tươi mát, giải khát',
      rating: 4.4,
      prepTime: '3 phút',
      popular: false
    }
  ];

  // Lọc menu theo danh mục và tìm kiếm
  const filteredMenu = menuItems.filter(item => {
    const matchesCategory = selectedCategory === 'all' || item.category === selectedCategory;
    const matchesSearch = item.name.toLowerCase().includes(searchTerm.toLowerCase());
    return matchesCategory && matchesSearch;
  });

  // Thêm vào giỏ hàng
  const addToCart = (item) => {
    const existingItem = cart.find(cartItem => cartItem.id === item.id);
    if (existingItem) {
      setCart(cart.map(cartItem => 
        cartItem.id === item.id 
          ? { ...cartItem, quantity: cartItem.quantity + 1 }
          : cartItem
      ));
    } else {
      setCart([...cart, { ...item, quantity: 1 }]);
    }
  };

  // Giảm số lượng trong giỏ hàng
  const removeFromCart = (itemId) => {
    const existingItem = cart.find(cartItem => cartItem.id === itemId);
    if (existingItem.quantity === 1) {
      setCart(cart.filter(cartItem => cartItem.id !== itemId));
    } else {
      setCart(cart.map(cartItem => 
        cartItem.id === itemId 
          ? { ...cartItem, quantity: cartItem.quantity - 1 }
          : cartItem
      ));
    }
  };

  // Tính tổng tiền
  const totalAmount = cart.reduce((total, item) => total + (item.price * item.quantity), 0);
  const totalItems = cart.reduce((total, item) => total + item.quantity, 0);

  // Lấy số lượng của một item trong giỏ hàng
  const getItemQuantity = (itemId) => {
    const item = cart.find(cartItem => cartItem.id === itemId);
    return item ? item.quantity : 0;
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

        {/* Categories */}
        <nav className="mt-6">
          <div className="px-6 py-2">
            <h3 className="text-xs font-semibold text-gray-500 uppercase tracking-wider">
              Danh mục món ăn
            </h3>
          </div>
          {categories.map((category) => (
            <button
              key={category.id}
              onClick={() => setSelectedCategory(category.id)}
              className={`w-full flex items-center gap-3 px-6 py-3 text-left transition-colors ${
                selectedCategory === category.id
                  ? 'bg-red-50 text-red-700 border-r-2 border-red-700'
                  : 'text-gray-700 hover:bg-gray-50'
              }`}
            >
              <category.icon size={20} />
              <span className="font-medium">{category.name}</span>
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
                  placeholder="Tìm kiếm món ăn..."
                  value={searchTerm}
                  onChange={(e) => setSearchTerm(e.target.value)}
                  className="pl-10 pr-4 py-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-red-500 focus:border-transparent w-80"
                />
              </div>
              <button className="flex items-center gap-2 px-4 py-2 border border-gray-300 rounded-lg hover:bg-gray-50">
                <Filter size={16} />
                <span>Bộ lọc</span>
              </button>
            </div>
            <div className="flex items-center gap-4">
              <button className="relative p-2 text-gray-600 hover:bg-gray-100 rounded-lg">
                <Bell size={20} />
                <span className="absolute -top-1 -right-1 w-5 h-5 bg-red-500 text-white text-xs rounded-full flex items-center justify-center">
                  3
                </span>
              </button>
              <div className="text-right">
                <p className="text-sm font-medium text-gray-900">Bàn số 5</p>
                <p className="text-xs text-gray-500">Đặt món</p>
              </div>
            </div>
          </div>
        </div>

        <div className="flex-1 flex">
          {/* Menu Content */}
          <div className="flex-1 overflow-auto p-6">
            <div className="mb-6">
              <h2 className="text-2xl font-bold text-gray-900 mb-2">Menu nhà hàng</h2>
              <p className="text-gray-600">Chọn món ăn yêu thích của bạn</p>
            </div>

            {/* Menu Grid */}
            <div className="grid grid-cols-2 gap-6">
              {filteredMenu.map((item) => (
                <div key={item.id} className="bg-white rounded-xl shadow-sm border border-gray-200 overflow-hidden hover:shadow-md transition-shadow">
                  <div className="relative">
                    <img 
                      src={item.image} 
                      alt={item.name}
                      className="w-full h-48 object-cover"
                    />
                    {item.popular && (
                      <div className="absolute top-3 left-3 bg-red-500 text-white px-2 py-1 rounded-full text-xs font-medium">
                        Phổ biến
                      </div>
                    )}
                  </div>
                  
                  <div className="p-4">
                    <div className="flex items-start justify-between mb-2">
                      <h3 className="font-semibold text-gray-900 text-lg">{item.name}</h3>
                      <div className="flex items-center gap-1 text-yellow-500">
                        <Star size={16} fill="currentColor" />
                        <span className="text-sm font-medium text-gray-700">{item.rating}</span>
                      </div>
                    </div>
                    
                    <p className="text-gray-600 text-sm mb-3 line-clamp-2">{item.description}</p>
                    
                    <div className="flex items-center gap-3 mb-4">
                      <div className="flex items-center gap-1 text-gray-500">
                        <Clock size={14} />
                        <span className="text-sm">{item.prepTime}</span>
                      </div>
                    </div>
                    
                    <div className="flex items-center justify-between">
                      <div className="text-xl font-bold text-red-600">
                        {item.price.toLocaleString('vi-VN')}đ
                      </div>
                      
                      <div className="flex items-center gap-2">
                        {getItemQuantity(item.id) > 0 ? (
                          <div className="flex items-center gap-2">
                            <button
                              onClick={() => removeFromCart(item.id)}
                              className="w-8 h-8 bg-red-100 text-red-600 rounded-full flex items-center justify-center hover:bg-red-200 transition-colors"
                            >
                              <Minus size={16} />
                            </button>
                            <span className="font-medium w-8 text-center">
                              {getItemQuantity(item.id)}
                            </span>
                            <button
                              onClick={() => addToCart(item)}
                              className="w-8 h-8 bg-red-500 text-white rounded-full flex items-center justify-center hover:bg-red-600 transition-colors"
                            >
                              <Plus size={16} />
                            </button>
                          </div>
                        ) : (
                          <button
                            onClick={() => addToCart(item)}
                            className="bg-red-500 text-white px-4 py-2 rounded-lg flex items-center gap-2 hover:bg-red-600 transition-colors"
                          >
                            <Plus size={16} />
                            <span>Thêm</span>
                          </button>
                        )}
                      </div>
                    </div>
                  </div>
                </div>
              ))}
            </div>

            {filteredMenu.length === 0 && (
              <div className="text-center py-12">
                <div className="text-gray-400 text-lg mb-2">Không tìm thấy món ăn nào</div>
                <div className="text-gray-500">Thử thay đổi từ khóa tìm kiếm hoặc danh mục</div>
              </div>
            )}
          </div>

          {/* Cart Sidebar */}
          <div className="w-80 bg-white border-l border-gray-200 flex flex-col">
            <div className="p-6 border-b border-gray-200">
              <div className="flex items-center gap-3">
                <ShoppingCart className="w-6 h-6 text-red-500" />
                <h3 className="text-lg font-semibold text-gray-900">
                  Đơn hàng ({totalItems})
                </h3>
              </div>
            </div>

            <div className="flex-1 overflow-auto p-6">
              {cart.length === 0 ? (
                <div className="text-center text-gray-500 mt-12">
                  <ShoppingCart className="w-12 h-12 mx-auto mb-4 text-gray-300" />
                  <p>Chưa có món nào trong đơn hàng</p>
                </div>
              ) : (
                <div className="space-y-4">
                  {cart.map((item) => (
                    <div key={item.id} className="flex items-center gap-3 bg-gray-50 rounded-lg p-3">
                      <img 
                        src={item.image} 
                        alt={item.name}
                        className="w-12 h-12 object-cover rounded-lg"
                      />
                      <div className="flex-1 min-w-0">
                        <h4 className="font-medium text-gray-900 truncate">{item.name}</h4>
                        <p className="text-sm text-red-600 font-medium">
                          {item.price.toLocaleString('vi-VN')}đ
                        </p>
                      </div>
                      <div className="flex items-center gap-2">
                        <button
                          onClick={() => removeFromCart(item.id)}
                          className="w-6 h-6 bg-red-100 text-red-600 rounded-full flex items-center justify-center hover:bg-red-200 transition-colors"
                        >
                          <Minus size={14} />
                        </button>
                        <span className="font-medium w-6 text-center text-sm">
                          {item.quantity}
                        </span>
                        <button
                          onClick={() => addToCart(item)}
                          className="w-6 h-6 bg-red-500 text-white rounded-full flex items-center justify-center hover:bg-red-600 transition-colors"
                        >
                          <Plus size={14} />
                        </button>
                      </div>
                    </div>
                  ))}
                </div>
              )}
            </div>

            {cart.length > 0 && (
              <div className="p-6 border-t border-gray-200">
                <div className="flex items-center justify-between mb-4">
                  <span className="text-lg font-semibold text-gray-900">Tổng cộng:</span>
                  <span className="text-2xl font-bold text-red-600">
                    {totalAmount.toLocaleString('vi-VN')}đ
                  </span>
                </div>
                <button className="w-full bg-red-500 text-white py-3 rounded-lg font-medium hover:bg-red-600 transition-colors">
                  Đặt món ({totalItems})
                </button>
              </div>
            )}
          </div>
        </div>
      </div>
    </div>
  );
};

export default OrderPage;