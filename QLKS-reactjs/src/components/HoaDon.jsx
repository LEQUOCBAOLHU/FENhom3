import React, { useEffect, useState } from 'react';
import { Table, Button, Modal, Form, Input, DatePicker, Space, Popconfirm, message, Select } from 'antd';
import { apiFetch } from '../auth';
import './HoaDon.css';

function HoaDon() {
  const [hoaDons, setHoaDons] = useState([]);
  const [loading, setLoading] = useState(false);
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [editingHoaDon, setEditingHoaDon] = useState(null);
  const [search, setSearch] = useState('');
  const [isStatusModalVisible, setIsStatusModalVisible] = useState(false);
  const [isPaymentModalVisible, setIsPaymentModalVisible] = useState(false);
  const [selectedHoaDon, setSelectedHoaDon] = useState(null);

  const fetchHoaDons = async () => {
    setLoading(true);
    try {
      let url = `http://localhost:5189/api/HoaDon`;
      if (search) url = `http://localhost:5189/api/HoaDon/khach-hang/${encodeURIComponent(search)}`;
      const res = await apiFetch(url);
      const data = await res.json();
      setHoaDons(Array.isArray(data) ? data : (data ? [data] : []));
    } catch (e) {
      setHoaDons([]);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => { fetchHoaDons(); }, [search]);

  const handleDelete = async (maHoaDon) => {
    await apiFetch(`http://localhost:5189/api/HoaDon/${maHoaDon}`, { method: 'DELETE' });
    message.success('Xóa hóa đơn thành công!');
    fetchHoaDons();
  };

  const showEditModal = (record) => {
    setEditingHoaDon(record);
    setIsModalVisible(true);
  };

  const showAddModal = () => {
    setEditingHoaDon(null);
    setIsModalVisible(true);
  };

  const handleOk = async (values) => {
    if (editingHoaDon) {
      await apiFetch(`http://localhost:5189/api/HoaDon/${editingHoaDon.maHoaDon}`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(values)
      });
      message.success('Cập nhật hóa đơn thành công!');
    } else {
      await apiFetch('http://localhost:5189/api/HoaDon', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(values)
      });
      message.success('Thêm hóa đơn thành công!');
    }
    setIsModalVisible(false);
    fetchHoaDons();
  };

  const handleUpdateStatus = async (values) => {
    await apiFetch(`http://localhost:5189/api/HoaDon/${selectedHoaDon.maHoaDon}/trang-thai`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ trangThai: values.trangThai })
    });
    message.success('Cập nhật trạng thái hóa đơn thành công!');
    setIsStatusModalVisible(false);
    fetchHoaDons();
  };

  const handleUpdatePayment = async (values) => {
    await apiFetch(`http://localhost:5189/api/HoaDon/${selectedHoaDon.maHoaDon}/phuong-thuc-thanh-toan`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ phuongThucThanhToan: values.phuongThucThanhToan })
    });
    message.success('Cập nhật phương thức thanh toán thành công!');
    setIsPaymentModalVisible(false);
    fetchHoaDons();
  };

  const handleExportPDFEmail = async () => {
    await apiFetch(`http://localhost:5189/api/HoaDon/${selectedHoaDon.maHoaDon}/export-pdf/email`, { method: 'POST' });
    message.success('Gửi hóa đơn PDF qua email thành công!');
  };

  const handleExportPDF = async (maHoaDon) => {
    await apiFetch(`http://localhost:5189/api/HoaDon/${maHoaDon}/export-pdf`, { method: 'POST' });
    message.success('Xuất PDF hóa đơn thành công!');
  };

  const columns = [
    { title: 'Mã hóa đơn', dataIndex: 'maHoaDon', key: 'maHoaDon' },
    { title: 'Khách hàng', dataIndex: 'tenKhachHang', key: 'tenKhachHang' },
    { title: 'Nhân viên', dataIndex: 'tenNhanVien', key: 'tenNhanVien' },
    { title: 'Ngày lập', dataIndex: 'ngayLap', key: 'ngayLap' },
    { title: 'Tổng tiền', dataIndex: 'tongTien', key: 'tongTien', render: v => v?.toLocaleString('vi-VN') },
    { title: 'Phương thức', dataIndex: 'phuongThucThanhToan', key: 'phuongThucThanhToan' },
    { title: 'Trạng thái', dataIndex: 'trangThai', key: 'trangThai' },
    {
      title: 'Hành động',
      key: 'action',
      render: (_, record) => (
        <Space>
          <Button onClick={() => showEditModal(record)}>Sửa</Button>
          <Popconfirm title="Bạn chắc chắn xóa?" onConfirm={() => handleDelete(record.maHoaDon)}>
            <Button danger>Xóa</Button>
          </Popconfirm>
          <Button onClick={() => handleExportPDF(record.maHoaDon)}>Xuất PDF</Button>
          <Button onClick={() => { setSelectedHoaDon(record); setIsStatusModalVisible(true); }}>Trạng thái</Button>
          <Button onClick={() => { setSelectedHoaDon(record); setIsPaymentModalVisible(true); }}>Phương thức TT</Button>
          <Button onClick={() => { setSelectedHoaDon(record); handleExportPDFEmail(); }}>Gửi Email</Button>
        </Space>
      )
    }
  ];

  return (
    <div style={{padding: 24}}>
      <h2>Quản lý hóa đơn</h2>
      <Space style={{marginBottom: 16}}>
        <Input.Search placeholder="Tìm kiếm theo tên khách hàng" onSearch={setSearch} allowClear />
        <Button type="primary" onClick={showAddModal}>Thêm hóa đơn</Button>
        <Button onClick={fetchHoaDons}>Làm mới</Button>
      </Space>
      <Table columns={columns} dataSource={hoaDons} rowKey="maHoaDon" loading={loading} />
      <Modal
        title={editingHoaDon ? 'Sửa hóa đơn' : 'Thêm hóa đơn'}
        open={isModalVisible}
        onCancel={() => setIsModalVisible(false)}
        footer={null}
        destroyOnClose
      >
        <Form
          layout="vertical"
          initialValues={editingHoaDon || {}}
          onFinish={handleOk}
        >
          <Form.Item label="Tên khách hàng" name="tenKhachHang" rules={[{ required: true, message: 'Nhập tên khách hàng!' }]}> <Input /> </Form.Item>
          <Form.Item label="Tên nhân viên" name="tenNhanVien" rules={[{ required: true, message: 'Nhập tên nhân viên!' }]}> <Input /> </Form.Item>
          <Form.Item label="Ngày lập" name="ngayLap" rules={[{ required: true, message: 'Chọn ngày lập!' }]}> <DatePicker style={{width:'100%'}} /> </Form.Item>
          <Form.Item label="Tổng tiền" name="tongTien" rules={[{ required: true, message: 'Nhập tổng tiền!' }]}> <Input type="number" /> </Form.Item>
          <Form.Item label="Phương thức thanh toán" name="phuongThucThanhToan" rules={[{ required: true, message: 'Chọn phương thức!' }]}> <Select><Select.Option value="Tiền mặt">Tiền mặt</Select.Option><Select.Option value="Chuyển khoản">Chuyển khoản</Select.Option></Select> </Form.Item>
          <Form.Item label="Trạng thái" name="trangThai" rules={[{ required: true, message: 'Chọn trạng thái!' }]}> <Select><Select.Option value="Đã thanh toán">Đã thanh toán</Select.Option><Select.Option value="Chưa thanh toán">Chưa thanh toán</Select.Option></Select> </Form.Item>
          <Form.Item> <Button type="primary" htmlType="submit">Lưu</Button> </Form.Item>
        </Form>
      </Modal>
      <Modal
        title="Cập nhật trạng thái hóa đơn"
        open={isStatusModalVisible}
        onCancel={() => setIsStatusModalVisible(false)}
        footer={null}
        destroyOnClose
      >
        <Form layout="vertical" onFinish={handleUpdateStatus}>
          <Form.Item label="Trạng thái" name="trangThai" rules={[{ required: true, message: 'Chọn trạng thái!' }]}> 
            <Select>
              <Select.Option value="Đã thanh toán">Đã thanh toán</Select.Option>
              <Select.Option value="Chưa thanh toán">Chưa thanh toán</Select.Option>
            </Select>
          </Form.Item>
          <Form.Item> <Button type="primary" htmlType="submit">Cập nhật</Button> </Form.Item>
        </Form>
      </Modal>
      <Modal
        title="Cập nhật phương thức thanh toán"
        open={isPaymentModalVisible}
        onCancel={() => setIsPaymentModalVisible(false)}
        footer={null}
        destroyOnClose
      >
        <Form layout="vertical" onFinish={handleUpdatePayment}>
          <Form.Item label="Phương thức thanh toán" name="phuongThucThanhToan" rules={[{ required: true, message: 'Chọn phương thức!' }]}> 
            <Select>
              <Select.Option value="Tiền mặt">Tiền mặt</Select.Option>
              <Select.Option value="Chuyển khoản">Chuyển khoản</Select.Option>
            </Select>
          </Form.Item>
          <Form.Item> <Button type="primary" htmlType="submit">Cập nhật</Button> </Form.Item>
        </Form>
      </Modal>
    </div>
  );
}

export default HoaDon;
