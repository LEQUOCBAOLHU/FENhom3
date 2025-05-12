import React, { useState, useEffect } from 'react';
import { useNavigate, Link, Routes, Route } from 'react-router-dom';
import { motion } from 'framer-motion';
import { BiMenu, BiSun, BiMoon, BiUser, BiBook, BiChart, BiLogOut, BiHome } from 'react-icons/bi';
import { useTheme } from '../contexts/ThemeContext';
import Phong from './Phong';
import DichVu from './DichVu';
import KhachHang from './KhachHang';
import BaoCao from './BaoCao';
import './Dashboard.css';

// Icons import
import { MdHotel, MdRoomService } from 'react-icons/md';

const menuItems = [
  { icon: <BiHome size={24} />, label: 'Dashboard', path: '/dashboard' },
  { icon: <MdHotel size={24} />, label: 'Phòng', path: '/dashboard/phong' },
  { icon: <MdRoomService size={24} />, label: 'Dịch vụ', path: '/dashboard/dichvu' },
  { icon: <BiUser size={24} />, label: 'Khách hàng', path: '/dashboard/khachhang' },
  { icon: <BiBook size={24} />, label: 'Báo cáo', path: '/dashboard/baocao' }
  // Đã xóa mục Thống kê
];

const StatCard = ({ icon, label, value, type }) => (
  <motion.div 
    className={`stat-card ${type}`}
    whileHover={{ scale: 1.02 }}
    whileTap={{ scale: 0.98 }}
  >
    <div className="stat-icon">{icon}</div>
    <div className="stat-info">
      <h3>{value}</h3>
      <p>{label}</p>
    </div>
  </motion.div>
);

const Dashboard = () => {
  const [isOpen, setIsOpen] = useState(true);
  const [isMobile, setIsMobile] = useState(window.innerWidth <= 768);
  const navigate = useNavigate();
  const { isDarkMode, toggleTheme } = useTheme();

  useEffect(() => {
    const handleResize = () => setIsMobile(window.innerWidth <= 768);
    window.addEventListener('resize', handleResize);
    return () => window.removeEventListener('resize', handleResize);
  }, []);

  const toggleSidebar = () => setIsOpen(!isOpen);

  const handleLogout = () => {
    localStorage.removeItem('user');
    localStorage.removeItem('token');
    localStorage.removeItem('refreshToken');
    navigate('/login');
  };

  const stats = [
    { icon: <MdHotel size={32} />, label: 'Phòng trống', value: '15', type: 'primary' },
    { icon: <MdRoomService size={32} />, label: 'Đang sử dụng', value: '8', type: 'success' },
    { icon: <BiUser size={32} />, label: 'Khách hàng', value: '24', type: 'warning' },
    { icon: <BiChart size={32} />, label: 'Doanh thu', value: '120M', type: 'info' },
  ];

  return (
    <div className="dashboard-container">
      {/* Sidebar */}
      <motion.div 
        className={`sidebar ${isOpen ? 'open' : ''}`}
        animate={{ width: isOpen ? '240px' : '80px' }}
        transition={{ duration: 0.3, type: "tween" }}
      >
        <div className="sidebar-header">
          <motion.h1 animate={{ opacity: isOpen ? 1 : 0 }}>
            QLKS
          </motion.h1>
          <button className="toggle-btn" onClick={toggleSidebar}>
            <BiMenu size={24} />
          </button>
        </div>

        <div className="sidebar-menu">
          {menuItems.map((item, index) => (
            <motion.div
              key={item.path}
              className="menu-item"
              whileHover={{ scale: 1.02, x: 5 }}
              whileTap={{ scale: 0.95 }}
            >
              <Link to={item.path} className="menu-link">
                {item.icon}
                <motion.span
                  animate={{ opacity: isOpen ? 1 : 0 }}
                  className="menu-label"
                >
                  {item.label}
                </motion.span>
              </Link>
            </motion.div>
          ))}
          
          {/* Theme toggle button */}
          <motion.div
            className="menu-item theme-toggle"
            whileHover={{ scale: 1.02, x: 5 }}
            whileTap={{ scale: 0.95 }}
          >
            <button className="menu-link" onClick={toggleTheme}>
              {isDarkMode ? <BiSun size={20} /> : <BiMoon size={20} />}
              <motion.span
                className="menu-label"
                animate={{ opacity: isOpen ? 1 : 0 }}
              >
                {isDarkMode ? 'Chế độ sáng' : 'Chế độ tối'}
              </motion.span>
            </button>
          </motion.div>

          {/* Logout Button */}
          <motion.div
            className="menu-item logout-item"
            whileHover={{ scale: 1.02, x: 5 }}
            whileTap={{ scale: 0.95 }}
          >
            <button onClick={handleLogout} className="menu-link logout-button">
              <BiLogOut size={24} />
              <motion.span
                animate={{ opacity: isOpen ? 1 : 0 }}
                className="menu-label"
              >
                Đăng xuất
              </motion.span>
            </button>
          </motion.div>
        </div>
      </motion.div>

      {/* Main Content */}
      <main className={`main-content ${isOpen ? 'sidebar-open' : ''}`}>
        <div className="top-bar">
          <button className="mobile-menu" onClick={toggleSidebar}>
            <BiMenu size={24} />
          </button>
          <div className="page-title">Dashboard</div>
        </div>

        <Routes>
          <Route path="" element={
            <div className="dashboard-content">
              <div className="welcome-section">
                <motion.h1
                  initial={{ opacity: 0, y: 20 }}
                  animate={{ opacity: 1, y: 0 }}
                  transition={{ duration: 0.5 }}
                >
                  Chào mừng đến với Hệ thống Quản lý Khách sạn
                </motion.h1>
              </div>

              <div className="stats-grid">
                {stats.map((stat, index) => (
                  <motion.div
                    key={index}
                    initial={{ opacity: 0, y: 20 }}
                    animate={{ opacity: 1, y: 0 }}
                    transition={{ delay: index * 0.1 }}
                  >
                    <StatCard {...stat} />
                  </motion.div>
                ))}
              </div>

              <div className="quick-actions">
                <motion.h2
                  initial={{ opacity: 0, x: -20 }}
                  animate={{ opacity: 1, x: 0 }}
                  transition={{ delay: 0.5 }}
                >
                  Thao tác nhanh
                </motion.h2>
                <div className="action-buttons">
                  {menuItems.slice(1).map((item, index) => (
                    <motion.button
                      key={item.path}
                      className="action-button"
                      whileHover={{ scale: 1.05 }}
                      whileTap={{ scale: 0.95 }}
                      onClick={() => navigate(item.path)}
                      initial={{ opacity: 0, y: 20 }}
                      animate={{ opacity: 1, y: 0 }}
                      transition={{ delay: 0.7 + index * 0.1 }}
                    >
                      {item.icon}
                      <span>{item.label}</span>
                    </motion.button>
                  ))}
                </div>
              </div>
            </div>
          } />
          <Route path="phong" element={<Phong />} />
          <Route path="dichvu" element={<DichVu />} />
          <Route path="khachhang" element={<KhachHang />} />
          <Route path="baocao" element={<BaoCao />} />
          <Route path="thongke" element={null} />
        </Routes>
      </main>
    </div>
  );
};

export default Dashboard;
