import React, { useEffect, useState } from 'react';
import { Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Typography, CircularProgress, Alert, Box, Button } from '@mui/material';
import { apiFetch } from '../auth';
import './DichVu.css';
import { Modal, Form, Input, Select } from 'antd';

const { Option } = Select;

function DichVu() {
  const [dichVus, setDichVus] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const [isModalVisible, setIsModalVisible] = useState(false);

  useEffect(() => {
    const fetchDichVus = async () => {
      setLoading(true);
      setError('');
      try {
        const res = await apiFetch('http://localhost:5189/api/DichVu');
        if (!res.ok) throw new Error('Không thể lấy dữ liệu dịch vụ');
        const data = await res.json();
        setDichVus(Array.isArray(data) ? data : []);
      } catch (err) {
        setError(err.message || 'Lỗi không xác định');
      } finally {
        setLoading(false);
      }
    };
    fetchDichVus();
  }, []);

  const handleAddEditDichVu = (values) => {
    console.log('Received values:', values);
    // Gọi API thêm/sửa dịch vụ ở đây
    setIsModalVisible(false);
  };

  return (
    <Box className="dich-vu-container">
      <Typography variant="h5" gutterBottom className="dich-vu-title">Quản lý Dịch vụ</Typography>
      <Button type="primary" onClick={() => setIsModalVisible(true)} className="add-button">
        Thêm Dịch Vụ
      </Button>
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
              {Array.isArray(dichVus) && dichVus.map((dv) => (
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
      {/* Modal thêm/sửa dịch vụ */}
      <Modal
        title="Thêm/Sửa Dịch Vụ"
        open={isModalVisible}
        onCancel={() => setIsModalVisible(false)}
        footer={null}
      >
        <Form layout="vertical" onFinish={handleAddEditDichVu}>
          <Form.Item label="Tên dịch vụ" name="tenDichVu" rules={[{ required: true, message: 'Vui lòng nhập tên dịch vụ' }]}>
            <Input placeholder="Nhập tên dịch vụ" />
          </Form.Item>
          <Form.Item label="Đơn giá" name="donGia" rules={[{ required: true, message: 'Vui lòng nhập đơn giá' }]}>
            <Input placeholder="Nhập đơn giá" type="number" />
          </Form.Item>
          <Form.Item label="Trạng thái" name="trangThai" rules={[{ required: true, message: 'Vui lòng chọn trạng thái' }]}>
            <Select placeholder="Chọn trạng thái">
              <Option value="Đang kinh doanh">Đang kinh doanh</Option>
              <Option value="Ngừng kinh doanh">Ngừng kinh doanh</Option>
            </Select>
          </Form.Item>
          <Form.Item>
            <Button type="primary" htmlType="submit">Lưu</Button>
          </Form.Item>
        </Form>
      </Modal>
    </Box>
  );
}

export default DichVu;
