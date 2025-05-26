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
  const [khachHangs, setKhachHangs] = useState([]);
  const [loadingKhachHang, setLoadingKhachHang] = useState(false);
  const [phongs, setPhongs] = useState([]);
  const [loadingPhong, setLoadingPhong] = useState(false);
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

  const showEditModal = async (record) => {
    setEditingDatPhong(record);
    await fetchKhachHangs();
    await fetchPhongs();
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

  // Lấy danh sách khách hàng còn hiệu lực
  const fetchKhachHangs = async () => {
    setLoadingKhachHang(true);
    try {
      const res = await apiFetch('http://localhost:5189/api/KhachHang?pageNumber=1&pageSize=100');
      const data = await res.json();
      // Lọc khách hàng còn hiệu lực (ví dụ: trangThai !== 'Vô hiệu hóa' hoặc isActive === true tuỳ backend)
      const list = Array.isArray(data) ? data : (data.khachHangs || []);
      const filtered = list.filter(kh => !kh.trangThai || kh.trangThai.toLowerCase() !== 'vô hiệu hóa');
      setKhachHangs(filtered);
    } catch (e) {
      setKhachHangs([]);
    } finally {
      setLoadingKhachHang(false);
    }
  };

  // Lấy danh sách phòng
  const fetchPhongs = async () => {
    setLoadingPhong(true);
    try {
      const res = await apiFetch('http://localhost:5189/api/Phong?pageNumber=1&pageSize=100');
      const data = await res.json();
      const list = Array.isArray(data) ? data : (data.phongs || []);
      setPhongs(list);
    } catch (e) {
      setPhongs([]);
    } finally {
      setLoadingPhong(false);
    }
  };

  const showAddModal = async () => {
    form.resetFields();
    setEditingDatPhong(null);
    // Lấy mã đặt phòng mới trước khi hiển thị form
    await fetchNewMaDatPhong();
    await fetchKhachHangs();
    await fetchPhongs();
    setIsModalVisible(true);
  };

  // Thêm hoặc sửa đặt phòng
  const handleOk = async (values) => {
    try {
      // Format ngày nhận và ngày trả về dạng ISO hoặc 'YYYY-MM-DD HH:mm' nếu cần
      const payloadData = {
        ...values,
        ngayNhanPhong: values.ngayNhanPhong ? dayjs(values.ngayNhanPhong).format('YYYY-MM-DD HH:mm') : undefined,
        ngayTraPhong: values.ngayTraPhong ? dayjs(values.ngayTraPhong).format('YYYY-MM-DD HH:mm') : undefined
      };
      if (editingDatPhong) {
        // Sửa
        const res = await apiFetch(`http://localhost:5189/api/DatPhong/${editingDatPhong.maDatPhong}`, {
          method: 'PUT',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify(payloadData)
        });
        if (!res.ok) throw new Error('Cập nhật thất bại');
        message.success('Cập nhật thành công!');
      } else {
        // Thêm mới: gửi đúng payload backend yêu cầu
        const payload = {
          DatPhongVMs: [payloadData],
          MaKhList: values.maKhList || []
        };
        const res = await apiFetch('http://localhost:5189/api/DatPhong', {
          method: 'POST',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify(payload)
        });
        if (!res.ok) throw new Error('Thêm mới thất bại');
        message.success('Thêm mới thành công!');
      }
      setIsModalVisible(false);
      setEditingDatPhong(null);
      form.resetFields();
      fetchDatPhongs();
    } catch (e) {
      message.error(e.message || 'Có lỗi xảy ra!');
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
        <Spin spinning={isLoadingMDP || loadingKhachHang}>
          <Form
            form={form}
            layout="vertical"
            onFinish={handleOk}
          >
            {/* <Form.Item
              label="Mã đặt phòng"
              name="maDatPhong"
              rules={[{ required: true, message: 'Mã đặt phòng là bắt buộc!' }]}
            >
              <Input disabled />
            </Form.Item> */}
            <Form.Item label="Mã phòng" name="maPhong" rules={[{ required: true, message: 'Chọn mã phòng!' }]}> 
              <Select
                placeholder="Chọn phòng"
                loading={loadingPhong}
                showSearch
                optionFilterProp="children"
                allowClear
              >
                {phongs.map(phong => (
                  <Select.Option key={phong.maPhong} value={phong.maPhong}>{phong.tenPhong} ({phong.maPhong})</Select.Option>
                ))}
              </Select>
            </Form.Item>
            <Form.Item label="Tên khách hàng" name="tenKhachHang" rules={[{ required: true, message: 'Chọn tên khách hàng!' }]}> 
              <Select
                placeholder="Chọn khách hàng"
                loading={loadingKhachHang}
                showSearch
                optionFilterProp="children"
                allowClear
              >
                {khachHangs.map(kh => (
                  <Select.Option key={kh.maKh} value={kh.hoTen}>{kh.hoTen}</Select.Option>
                ))}
              </Select>
            </Form.Item>
            <Form.Item label="Ngày nhận phòng" name="ngayNhanPhong" rules={[{ required: true, message: 'Chọn ngày nhận!' }]}> 
              <DatePicker style={{width:'100%'}} showTime format="YYYY-MM-DD HH:mm" /> 
            </Form.Item>
            <Form.Item label="Ngày trả phòng" name="ngayTraPhong" rules={[{ required: true, message: 'Chọn ngày trả!' }]}> 
              <DatePicker style={{width:'100%'}} showTime format="YYYY-MM-DD HH:mm" /> 
            </Form.Item>
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
            <Form.Item label="Khách hàng (nhiều)" name="maKhList">
              <Select
                mode="multiple"
                placeholder="Chọn khách hàng"
                loading={loadingKhachHang}
                optionFilterProp="children"
                showSearch
                allowClear
              >
                {khachHangs.map(kh => (
                  <Select.Option key={kh.maKh} value={kh.maKh}>{kh.hoTen}</Select.Option>
                ))}
              </Select>
            </Form.Item>
            <Form.Item> <Button type="primary" htmlType="submit">Lưu</Button> </Form.Item>
          </Form>
        </Spin>
      </Modal>
    </div>
  );
}

export default DatPhong;
