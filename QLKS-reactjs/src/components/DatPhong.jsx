import React, { useEffect, useState } from 'react';
import { Table, Button, Modal, Form, Input, DatePicker, Space, Popconfirm, message, Select, InputNumber, Spin } from 'antd';
import { apiFetch } from '../auth';
import dayjs from 'dayjs';
import './DatPhong.css';

function DatPhong() {
  const [datPhongs, setDatPhongs] = useState([]);
  const [loading, setLoading] = useState(false);
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [editingDatPhong, setEditingDatPhong] = useState(null);
  const [search, setSearch] = useState('');
  const [newMaDatPhong, setNewMaDatPhong] = useState("");
  const [isLoadingMDP, setIsLoadingMDP] = useState(false);
  const [form] = Form.useForm();

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

  const fetchNewMaDatPhong = async () => {
    setIsLoadingMDP(true);
    try {
      const res = await apiFetch("http://localhost:5189/api/DatPhong/new-ma-dat-phong");
      const data = await res.json();
      if (data && data.maDatPhong) {
        setNewMaDatPhong(data.maDatPhong);
        // Set giá trị mã đặt phòng và trạng thái mặc định
        form.setFieldsValue({
          maDatPhong: data.maDatPhong,
          trangThai: 'Đã đặt'
        });
      }
    } catch (error) {
      console.error("Lỗi khi lấy mã đặt phòng:", error);
      message.error("Không thể lấy mã đặt phòng mới");
    } finally {
      setIsLoadingMDP(false);
    }
  };

  const showAddModal = async () => {
    form.resetFields();
    setEditingDatPhong(null);
    // Lấy mã đặt phòng mới trước khi hiển thị form
    await fetchNewMaDatPhong();
    setIsModalVisible(true);
  };

  const handleOk = async (values) => {
    try {
      if (!values.maDatPhong && !newMaDatPhong) {
        message.error('Không có mã đặt phòng!');
        return;
      }

      const formData = {
        maDatPhong: values.maDatPhong || newMaDatPhong,
        maPhong: values.maPhong,
        tenKhachHang: values.tenKhachHang,
        ngayNhanPhong: dayjs(values.ngayNhanPhong).format('YYYY-MM-DD'),
        ngayTraPhong: dayjs(values.ngayTraPhong).format('YYYY-MM-DD'),
        trangThai: values.trangThai || 'Đã đặt',
        soNguoiO: values.soNguoiO || 1
      };

      const nhanVienId = localStorage.getItem('nhanVienId');
      if (nhanVienId) {
        formData.maNv = parseInt(nhanVienId);
      }

      console.log('Dữ liệu gửi đi:', formData);

      if (editingDatPhong) {
        const res = await apiFetch(`http://localhost:5189/api/DatPhong/${editingDatPhong.maDatPhong}`, {
          method: 'PUT',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify(formData)
        });
        
        if (!res.ok) {
          const error = await res.json();
          throw new Error(error?.message || 'Cập nhật thất bại');
        }
        message.success('Cập nhật đặt phòng thành công!');
      } else {
        const res = await apiFetch('http://localhost:5189/api/DatPhong', {
          method: 'POST',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify({
            DatPhongVMs: [formData],
            MaKhList: []
          })
        });

        if (!res.ok) {
          const error = await res.json();
          throw new Error(error?.message || 'Thêm mới thất bại');
        }
        message.success('Thêm đặt phòng thành công!');
      }

      setIsModalVisible(false);
      form.resetFields();
      fetchDatPhongs();
    } catch (error) {
      message.error(error.message || 'Có lỗi xảy ra!');
      console.error('Lỗi khi lưu:', error);
    }
  };

  const handleUpdateRoomStatus = async (maPhong, trangThai) => {
    try {
      await apiFetch(`http://localhost:5189/api/DatPhong/rooms/${maPhong}/status`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(trangThai)
      });
      message.success('Cập nhật trạng thái phòng thành công!');
      fetchDatPhongs();
    } catch (e) {
      message.error('Cập nhật trạng thái phòng thất bại!');
    }
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
    },
    {
      title: 'Cập nhật trạng thái phòng',
      key: 'updateRoomStatus',
      render: (_, record) => (
        <Select
          defaultValue={record.trangThai}
          style={{ width: 140 }}
          onChange={value => handleUpdateRoomStatus(record.maPhong, value)}
        >
          <Select.Option value="Đang sử dụng">Đang sử dụng</Select.Option>
          <Select.Option value="Hủy">Hủy</Select.Option>
          <Select.Option value="Hoàn thành">Hoàn thành</Select.Option>
          <Select.Option value="Đã đặt">Đã đặt</Select.Option>
        </Select>
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
        onCancel={() => {
          setIsModalVisible(false);
          form.resetFields();
        }}
        footer={null}
        destroyOnClose
      >
        <Spin spinning={isLoadingMDP}>
          <Form
            form={form}
            layout="vertical"
            onFinish={handleOk}
          >
            <Form.Item
              label="Mã đặt phòng"
              name="maDatPhong"
              rules={[{ required: true, message: 'Mã đặt phòng là bắt buộc!' }]}
            >
              <Input disabled />
            </Form.Item>
            <Form.Item label="Mã phòng" name="maPhong" rules={[{ required: true, message: 'Nhập mã phòng!' }]}> <Input /> </Form.Item>
            <Form.Item label="Tên khách hàng" name="tenKhachHang" rules={[{ required: true, message: 'Nhập tên khách hàng!' }]}> <Input /> </Form.Item>
            <Form.Item label="Ngày nhận phòng" name="ngayNhanPhong" rules={[{ required: true, message: 'Chọn ngày nhận!' }]}> <DatePicker style={{width:'100%'}} /> </Form.Item>
            <Form.Item label="Ngày trả phòng" name="ngayTraPhong" rules={[{ required: true, message: 'Chọn ngày trả!' }]}> <DatePicker style={{width:'100%'}} /> </Form.Item>
            <Form.Item label="Trạng thái" name="trangThai" rules={[{ required: true, message: 'Chọn trạng thái!' }]}> 
              <Select placeholder="Chọn trạng thái">
                <Select.Option value="Đang sử dụng">Đang sử dụng</Select.Option>
                <Select.Option value="Hủy">Hủy</Select.Option>
                <Select.Option value="Hoàn thành">Hoàn thành</Select.Option>
                <Select.Option value="Đã đặt">Đã đặt</Select.Option>
              </Select>
            </Form.Item>
            <Form.Item label="Số người ở" name="soNguoiO" rules={[{ required: true, type: 'number', min: 1, message: 'Nhập số người ở (>=1)!' }]}> 
              <InputNumber min={1} style={{ width: '100%' }} /> 
            </Form.Item>
            <Form.Item> <Button type="primary" htmlType="submit">Lưu</Button> </Form.Item>
          </Form>
        </Spin>
      </Modal>
    </div>
  );
}

export default DatPhong;
