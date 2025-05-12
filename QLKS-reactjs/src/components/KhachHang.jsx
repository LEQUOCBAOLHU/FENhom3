import React, { useEffect, useState } from 'react';
import { Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Typography, CircularProgress, Alert, Box } from '@mui/material';
import { apiFetch } from '../auth';
import './KhachHang.css';

function KhachHang() {
  const [khachHangs, setKhachHangs] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');

  useEffect(() => {
    const fetchKhachHangs = async () => {
      setLoading(true);
      setError('');
      try {
        const res = await apiFetch('http://localhost:5189/api/KhachHang/get-all');
        if (!res.ok) throw new Error('Không thể lấy dữ liệu khách hàng');
        const data = await res.json();
        setKhachHangs(data);
      } catch (err) {
        setError(err.message || 'Lỗi không xác định');
      } finally {
        setLoading(false);
      }
    };
    fetchKhachHangs();
  }, []);

  return (
    <div className="khach-hang-container">
      <Typography variant="h5" gutterBottom>Quản lý Khách hàng</Typography>
      {loading && <Box sx={{display:'flex',justifyContent:'center',my:4}}><CircularProgress /></Box>}
      {error && <Alert severity="error">{error}</Alert>}
      {!loading && !error && (
        <TableContainer component={Paper} sx={{ maxHeight: 500 }}>
          <Table stickyHeader aria-label="bảng khách hàng">
            <TableHead>
              <TableRow>
                <TableCell>Mã KH</TableCell>
                <TableCell>Họ tên</TableCell>
                <TableCell>Số điện thoại</TableCell>
                <TableCell>CCCD/Passport</TableCell>
                <TableCell>Quốc tịch</TableCell>
                <TableCell>Ghi chú</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {khachHangs.map((kh) => (
                <TableRow key={kh.maKh || kh.hoTen} hover>
                  <TableCell>{kh.maKh || ''}</TableCell>
                  <TableCell>{kh.hoTen}</TableCell>
                  <TableCell>{kh.soDienThoai}</TableCell>
                  <TableCell>{kh.cccdPassport}</TableCell>
                  <TableCell>{kh.quocTich}</TableCell>
                  <TableCell>{kh.ghiChu}</TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </TableContainer>
      )}
    </div>
  );
}

export default KhachHang;
