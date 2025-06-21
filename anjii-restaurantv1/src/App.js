import React from "react";
import {
  BrowserRouter as Router,
  Routes,
  Route,
  Navigate,
} from "react-router-dom";
import AdminLayout from "./layouts/AdminLayout";
import Dashboard from "./pages/admin/Dashboard";
import UserManagement from "./pages/admin/StaffManagement";
import MenuManagement from "./pages/admin/MenuManagement";
import OrderManagement from "./pages/admin/OrderManagement";
import PromotionManagement from "./pages/admin/PromotionManagement";
import Revenue from "./pages/admin/Revenue";
import "./App.css";

function App() {
  return (
    <Router>
      <Routes>
        {/* Admin Routes */}
        <Route
          path="/admin"
          element={
            <AdminLayout>
              <Dashboard />
            </AdminLayout>
          }
        />
        <Route
          path="/admin/users"
          element={
            <AdminLayout>
              <UserManagement />
            </AdminLayout>
          }
        />
        <Route
          path="/admin/menu"
          element={
            <AdminLayout>
              <MenuManagement />
            </AdminLayout>
          }
        />
        <Route
          path="/admin/orders"
          element={
            <AdminLayout>
              <OrderManagement />
            </AdminLayout>
          }
        />
        <Route
          path="/admin/revenue"
          element={
            <AdminLayout>
              <Revenue />
            </AdminLayout>
          }
        />
        <Route
          path="/admin/promotion"
          element={
            <AdminLayout>
              <PromotionManagement />
            </AdminLayout>
          }
        />

        {/* Redirect root to admin dashboard for now */}
        <Route path="/" element={<Navigate to="/admin" replace />} />
      </Routes>
    </Router>
  );
}

export default App;
