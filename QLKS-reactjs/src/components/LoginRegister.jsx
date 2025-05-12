import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { motion } from 'framer-motion';
import { useTheme } from '../contexts/ThemeContext';
import { saveAuthTokens } from '../auth';
import './LoginRegister.css';
import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';
import TextField from '@mui/material/TextField';
import Button from '@mui/material/Button';

const LoginRegister = () => {
  const [isLogin, setIsLogin] = useState(true);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [message, setMessage] = useState('');
  const navigate = useNavigate();
  const { isDarkMode } = useTheme();

  const handleSubmit = async (e) => {
    e.preventDefault();
    setLoading(true);
    setError('');
    setMessage('');

    const payload = { Email: email, MatKhau: password };

    try {
      const response = await fetch('http://localhost:5189/api/Auth/login', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(payload),
      });

      const data = await response.json();

      if (response.ok) {
        if (!data.token || !data.refreshToken) {
          throw new Error('Thông tin xác thực không đầy đủ.');
        }
        await saveAuthTokens(data.token, data.refreshToken);
        localStorage.setItem('user', JSON.stringify({ email: data.email, hoTen: data.hoTen }));
        setMessage('Đăng nhập thành công!');
        navigate('/Dashboard');
      } else {
        setMessage(data.Message || 'Đăng nhập thất bại. Vui lòng kiểm tra email và mật khẩu.');
      }
    } catch (error) {
      setMessage('Không thể kết nối tới hệ thống. Vui lòng thử lại sau.');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className={`auth-container ${isDarkMode ? 'dark' : 'light'}`}>
      <motion.div
        initial={{ opacity: 0, y: 20 }}
        animate={{ opacity: 1, y: 0 }}
        transition={{ duration: 0.5 }}
        className="auth-box"
      >
        <Typography variant="h4" component="h2" sx={{ color: '#4f46e5', fontWeight: 700, textAlign: 'center', mb: 3 }}>
          {isLogin ? 'Đăng Nhập' : 'Đăng Ký'}
        </Typography>
        <form onSubmit={handleSubmit}>
          <div className="form-group">
            <TextField
              label="Email"
              type="email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              required
              fullWidth
              variant="outlined"
            />
          </div>
          <div className="form-group">
            <TextField
              label="Mật Khẩu"
              type="password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              required
              fullWidth
              variant="outlined"
            />
          </div>
          {!isLogin && (
            <div className="form-group">
              <TextField
                label="Xác Nhận Mật Khẩu"
                type="password"
                required
                fullWidth
                variant="outlined"
              />
            </div>
          )}
          {error && <div className="auth-message error">{error}</div>}
          <Button
            type="submit"
            variant="contained"
            color="primary"
            size="large"
            sx={{ fontWeight: 700, width: '100%', height: 54 }}
            disabled={loading}
          >
            {loading ? 'Đang xử lý...' : isLogin ? 'Đăng Nhập' : 'Đăng Ký'}
          </Button>
        </form>
        <Button
          className="auth-link"
          onClick={() => setIsLogin(!isLogin)}
          sx={{ textTransform: 'none', mt: 2 }}
        >
          {isLogin ? 'Chưa có tài khoản? Đăng ký ngay' : 'Đã có tài khoản? Đăng nhập'}
        </Button>
      </motion.div>
    </div>
  );
};

export default LoginRegister;
