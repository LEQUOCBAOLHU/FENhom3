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
<<<<<<< HEAD
=======
  const [selectedHoaDon, setSelectedHoaDon] = useState(null);
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c

  // Lấy id nhân viên từ localStorage (sau khi login backend đã trả về)
  const nhanVienId = localStorage.getItem('nhanVienId');

  const fetchHoaDons = async () => {
    setLoading(true);
    try {
      let url = `http://localhost:5189/api/HoaDon?pageNumber=1&pageSize=10`;
      if (search) url = `http://localhost:5189/api/HoaDon/khach-hang/${encodeURIComponent(search)}`;
      const res = await apiFetch(url);
      const data = await res.json();
<<<<<<< HEAD
      // Lấy đúng mảng hóa đơn từ backend (dạng phân trang)
      const list = Array.isArray(data)
        ? data
        : (data.data?.hoaDons || data.hoaDons || data.HoaDons || []);
=======
      // Nếu backend trả về dạng { hoaDons: [...] } thì lấy data.hoaDons, nếu trả về mảng thì lấy trực tiếp
      const list = Array.isArray(data) ? data : (data.hoaDons || []);
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
      setHoaDons(list);
    } catch (e) {
      setHoaDons([]);
    } finally {
      setLoading(false);
    }
  };

<<<<<<< HEAD
  useEffect(() => { fetchHoaDons(); }, [search]); // eslint-disable-line
=======
  useEffect(() => { fetchHoaDons(); }, [search]);
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c

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
    // Khi tạo mới, gửi idNhanVien thay vì tên, không gửi tổng tiền phòng khi update
    let body = { ...values };
    if (editingHoaDon) {
      // Không gửi/tính lại tổng tiền phòng khi update
      delete body.tongTien;
      await apiFetch(`http://localhost:5189/api/HoaDon/${editingHoaDon.maHoaDon}`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(body)
      });
      message.success('Cập nhật hóa đơn thành công!');
    } else {
      // Gửi idNhanVien khi tạo mới
      body.idNhanVien = nhanVienId;
      await apiFetch('http://localhost:5189/api/HoaDon', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(body)
      });
      message.success('Thêm hóa đơn thành công!');
    }
    setIsModalVisible(false);
    fetchHoaDons();
  };

<<<<<<< HEAD
  // Thay selectedHoaDon thành editingHoaDon cho các hàm cập nhật trạng thái/phương thức thanh toán
  const handleUpdateStatus = async (values) => {
    if (!editingHoaDon) return;
    await apiFetch(`http://localhost:5189/api/HoaDon/${editingHoaDon.maHoaDon}/trang-thai`, {
=======
  const handleUpdateStatus = async (values) => {
    await apiFetch(`http://localhost:5189/api/HoaDon/${selectedHoaDon.maHoaDon}/trang-thai`, {
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ trangThai: values.trangThai })
    });
    message.success('Cập nhật trạng thái hóa đơn thành công!');
    setIsStatusModalVisible(false);
    fetchHoaDons();
  };

  const handleUpdatePayment = async (values) => {
<<<<<<< HEAD
    if (!editingHoaDon) return;
    await apiFetch(`http://localhost:5189/api/HoaDon/${editingHoaDon.maHoaDon}/phuong-thuc-thanh-toan`, {
=======
    await apiFetch(`http://localhost:5189/api/HoaDon/${selectedHoaDon.maHoaDon}/phuong-thuc-thanh-toan`, {
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ phuongThucThanhToan: values.phuongThucThanhToan })
    });
    message.success('Cập nhật phương thức thanh toán thành công!');
    setIsPaymentModalVisible(false);
    fetchHoaDons();
  };

  const handleExportPDF = async (maHoaDon) => {
    try {
      const res = await apiFetch(`http://localhost:5189/api/HoaDon/${maHoaDon}/export-pdf`, { method: 'POST' });
      if (!res.ok) throw new Error('Xuất PDF thất bại');
      const blob = await res.blob();
      const url = window.URL.createObjectURL(blob);
      const a = document.createElement('a');
      a.href = url;
      a.download = `HoaDon_${maHoaDon}.pdf`;
      a.click();
      window.URL.revokeObjectURL(url);
    } catch (e) {
      message.error('Xuất PDF thất bại!');
    }
  };

  const handleExportPDFEmail = async (maHoaDon) => {
    try {
      const res = await apiFetch(`http://localhost:5189/api/HoaDon/${maHoaDon}/export-pdf/email`, { method: 'POST' });
      if (!res.ok) throw new Error('Gửi email thất bại');
      message.success('Đã gửi hóa đơn PDF qua email!');
    } catch (e) {
      message.error('Gửi email thất bại!');
    }
  };

  const columns = [
    { title: 'Mã hóa đơn', dataIndex: 'maHoaDon', key: 'maHoaDon' },
    { title: 'Khách hàng', dataIndex: 'tenKhachHang', key: 'tenKhachHang' },
    { title: 'Nhân viên', dataIndex: 'idNhanVien', key: 'idNhanVien' },
    { title: 'Ngày lập', dataIndex: 'ngayLap', key: 'ngayLap' },
    { title: 'Tổng tiền', dataIndex: 'tongTien', key: 'tongTien', render: v => v?.toLocaleString('vi-VN') },
    { title: 'Phụ thu', dataIndex: 'phuThu', key: 'phuThu', render: v => v ? v.toLocaleString('vi-VN') + ' đ' : '' },
    { title: 'Tiền theo giờ', dataIndex: 'tienTheoGio', key: 'tienTheoGio', render: v => v ? v.toLocaleString('vi-VN') + ' đ' : '' },
    { title: 'Tiền theo ngày', dataIndex: 'tienTheoNgay', key: 'tienTheoNgay', render: v => v ? v.toLocaleString('vi-VN') + ' đ' : '' },
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
          <Button onClick={() => handleExportPDFEmail(record.maHoaDon)}>Gửi PDF Email</Button>
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
