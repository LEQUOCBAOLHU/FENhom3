import React, { useEffect, useState } from 'react';
import { Table, Button, Modal, Form, Input, DatePicker, Space, Popconfirm, message } from 'antd';
import { apiFetch } from '../auth';
import './DatPhong.css';

function DatPhong() {
  const [datPhongs, setDatPhongs] = useState([]);
  const [loading, setLoading] = useState(false);
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [editingDatPhong, setEditingDatPhong] = useState(null);
  const [search, setSearch] = useState('');

  const fetchDatPhongs = async () => {
    setLoading(true);
    try {
      let url = `http://localhost:5189/api/DatPhong`;
      const res = await apiFetch(url);
      const data = await res.json();
      setDatPhongs(Array.isArray(data) ? data : (data ? [data] : []));
    } catch (e) {
      setDatPhongs([]);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => { fetchDatPhongs(); }, []);

  const handleDelete = async (maDatPhong) => {
    await apiFetch(`http://localhost:5189/api/DatPhong/${maDatPhong}`, { method: 'DELETE' });
    message.success('Xóa đặt phòng thành công!');
    fetchDatPhongs();
  };

  const showEditModal = (record) => {
    setEditingDatPhong(record);
    setIsModalVisible(true);
  };

  const showAddModal = () => {
    setEditingDatPhong(null);
    setIsModalVisible(true);
  };

  const handleOk = async (values) => {
    if (editingDatPhong) {
      // Sửa đặt phòng
      await apiFetch(`http://localhost:5189/api/DatPhong/${editingDatPhong.maDatPhong}`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(values)
      });
      message.success('Cập nhật đặt phòng thành công!');
    } else {
      // Thêm đặt phòng
      await apiFetch('http://localhost:5189/api/DatPhong', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify([values])
      });
      message.success('Thêm đặt phòng thành công!');
    }
    setIsModalVisible(false);
    fetchDatPhongs();
  };

  const columns = [
    { title: 'Mã đặt phòng', dataIndex: 'maDatPhong', key: 'maDatPhong' },
    { title: 'Khách hàng', dataIndex: 'tenKhachHang', key: 'tenKhachHang' },
    { title: 'Phòng', dataIndex: 'maPhong', key: 'maPhong' },
    { title: 'Ngày nhận', dataIndex: 'ngayNhanPhong', key: 'ngayNhanPhong' },
    { title: 'Ngày trả', dataIndex: 'ngayTraPhong', key: 'ngayTraPhong' },
    { title: 'Trạng thái', dataIndex: 'trangThai', key: 'trangThai' },
    {
      title: 'Hành động',
      key: 'action',
      render: (_, record) => (
        <Space>
          <Button onClick={() => showEditModal(record)}>Sửa</Button>
          <Popconfirm title="Bạn chắc chắn xóa?" onConfirm={() => handleDelete(record.maDatPhong)}>
            <Button danger>Xóa</Button>
          </Popconfirm>
        </Space>
      )
    }
  ];

  return (
    <div style={{padding: 24}}>
      <h2>Quản lý đặt phòng</h2>
      <Space style={{marginBottom: 16}}>
        <Button type="primary" onClick={showAddModal}>Thêm đặt phòng</Button>
        <Button onClick={fetchDatPhongs}>Làm mới</Button>
      </Space>
      <Table columns={columns} dataSource={datPhongs} rowKey="maDatPhong" loading={loading} />
      <Modal
        title={editingDatPhong ? 'Sửa đặt phòng' : 'Thêm đặt phòng'}
        open={isModalVisible}
        onCancel={() => setIsModalVisible(false)}
        footer={null}
        destroyOnClose
      >
        <Form
          layout="vertical"
          initialValues={editingDatPhong || {}}
          onFinish={handleOk}
        >
          <Form.Item label="Mã phòng" name="maPhong" rules={[{ required: true, message: 'Nhập mã phòng!' }]}> <Input /> </Form.Item>
          <Form.Item label="Tên khách hàng" name="tenKhachHang" rules={[{ required: true, message: 'Nhập tên khách hàng!' }]}> <Input /> </Form.Item>
          <Form.Item label="Ngày nhận phòng" name="ngayNhanPhong" rules={[{ required: true, message: 'Chọn ngày nhận!' }]}> <DatePicker style={{width:'100%'}} /> </Form.Item>
          <Form.Item label="Ngày trả phòng" name="ngayTraPhong" rules={[{ required: true, message: 'Chọn ngày trả!' }]}> <DatePicker style={{width:'100%'}} /> </Form.Item>
          <Form.Item label="Trạng thái" name="trangThai" rules={[{ required: true, message: 'Chọn trạng thái!' }]}> <Input /> </Form.Item>
          <Form.Item> <Button type="primary" htmlType="submit">Lưu</Button> </Form.Item>
        </Form>
      </Modal>
    </div>
  );
}

export default DatPhong;
