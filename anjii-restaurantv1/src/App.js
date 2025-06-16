import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';

import AnJiiStaffHomepage from './pages/AnJiiStaffHomepage'; // sửa lại nếu cần
import AnJiiStaffOrderpage from './pages/AnJiiStaffOrderpage'; // sửa lại nếu cần

export default function App() {
  return (
    <Router>
      <Routes>
        <Route path="/staff" element={<AnJiiStaffHomepage />} />
        <Route path="/staff/orders" element={<AnJiiStaffOrderpage />} />
      </Routes>
    </Router>
  );
}
