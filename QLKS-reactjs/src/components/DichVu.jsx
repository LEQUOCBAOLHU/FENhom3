import React, { useEffect, useState } from 'react';
import { Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Typography, CircularProgress, Alert, Box } from '@mui/material';
import { apiFetch } from '../auth';
import './DichVu.css';

function DichVu() {
  const [dichVus, setDichVus] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');

  useEffect(() => {
    const fetchDichVus = async () => {
      setLoading(true);
      setError('');
      try {
        const res = await apiFetch('http://localhost:5189/api/DichVu/get-all');
        if (!res.ok) throw new Error('Không thể lấy dữ liệu dịch vụ');
        const data = await res.json();
        setDichVus(data);
      } catch (err) {
        setError(err.message || 'Lỗi không xác định');
      } finally {
        setLoading(false);
      }
    };
    fetchDichVus();
  }, []);

  return (
    <Box className="dich-vu-container">
      <Typography variant="h5" gutterBottom className="dich-vu-title">Quản lý Dịch vụ</Typography>
      {loading && <Box className="loading-container"><CircularProgress /></Box>}
      {error && <Alert severity="error" className="error-alert">{error}</Alert>}
      {!loading && !error && (
        <TableContainer component={Paper} className="table-container" sx={{ maxHeight: 500 }}>
          <Table stickyHeader aria-label="bảng dịch vụ" className="table">
            <TableHead>
              <TableRow>
                <TableCell className="table-header-cell">Mã dịch vụ</TableCell>
                <TableCell className="table-header-cell">Tên dịch vụ</TableCell>
                <TableCell className="table-header-cell">Đơn giá</TableCell>
                <TableCell className="table-header-cell">Mô tả</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {dichVus.map((dv) => (
                <TableRow key={dv.maDichVu || dv.tenDichVu} hover className="table-row">
                  <TableCell className="table-cell">{dv.maDichVu || ''}</TableCell>
                  <TableCell className="table-cell">{dv.tenDichVu}</TableCell>
                  <TableCell className="table-cell">{dv.donGia}</TableCell>
                  <TableCell className="table-cell">{dv.moTa}</TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </TableContainer>
      )}
    </Box>
  );
}

export default DichVu;
