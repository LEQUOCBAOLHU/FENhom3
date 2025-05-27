<<<<<<< HEAD
import React, { useState, useEffect } from 'react';
import { Table, Button, Modal, Form, DatePicker, Space, Popconfirm, message, Select, InputNumber, Spin } from 'antd';
=======
import React, { useEffect, useState } from 'react';
import { Table, Button, Modal, Form, Input, DatePicker, Space, Popconfirm, message, Select, InputNumber, Spin } from 'antd';
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
import { apiFetch } from '../auth';
import dayjs from 'dayjs';
import './DatPhong.css';

function DatPhong() {
  const [datPhongs, setDatPhongs] = useState([]);
  const [loading, setLoading] = useState(false);
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [editingDatPhong, setEditingDatPhong] = useState(null);
<<<<<<< HEAD
  const [phongs, setPhongs] = useState([]);
  const [khachHangs, setKhachHangs] = useState([]);
  const [loadingPhong, setLoadingPhong] = useState(false);
  const [loadingKhachHang, setLoadingKhachHang] = useState(false);
  const [form] = Form.useForm();

  // Lấy danh sách đặt phòng
  const fetchDatPhongs = async () => {
    setLoading(true);
    try {
      const res = await apiFetch('http://localhost:5189/api/DatPhong?pageNumber=1&pageSize=20');
      const data = await res.json();
      console.log('API Response (fetchDatPhongs):', JSON.stringify(data, null, 2)); // Debug API response
      let list = [];
      if (data && data.data && Array.isArray(data.data.datPhongs)) {
        list = data.data.datPhongs;
      } else if (data && Array.isArray(data.datPhongs)) {
        list = data.datPhongs;
      } else if (data && Array.isArray(data.DatPhongs)) {
        list = data.DatPhongs;
      } else if (Array.isArray(data)) {
        list = data;
      }
      setDatPhongs(list);
      if (list.length === 0) message.warning('Không có dữ liệu đặt phòng!');
    } catch (e) {
      console.error('Error fetching datPhongs:', e);
      setDatPhongs([]);
      message.error('Không thể lấy dữ liệu đặt phòng!');
=======
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
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
    } finally {
      setLoading(false);
    }
  };

<<<<<<< HEAD
=======
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

>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
  // Lấy danh sách phòng
  const fetchPhongs = async () => {
    setLoadingPhong(true);
    try {
      const res = await apiFetch('http://localhost:5189/api/Phong?pageNumber=1&pageSize=100');
      const data = await res.json();
<<<<<<< HEAD
      console.log('API Response (fetchPhongs):', JSON.stringify(data, null, 2)); // Debug API response
      let list = [];
      if (data && data.data && Array.isArray(data.data.phongs)) {
        list = data.data.phongs;
      } else if (data && Array.isArray(data.phongs)) {
        list = data.phongs;
      } else if (data && Array.isArray(data.Phongs)) {
        list = data.Phongs;
      } else if (Array.isArray(data)) {
        list = data;
      }
      setPhongs(list);
      if (list.length === 0) message.warning('Không có phòng nào trong hệ thống!');
    } catch (e) {
      console.error('Error fetching phongs:', e);
      setPhongs([]);
      message.error('Không thể lấy dữ liệu phòng!');
=======
      const list = Array.isArray(data) ? data : (data.phongs || []);
      setPhongs(list);
    } catch (e) {
      setPhongs([]);
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
    } finally {
      setLoadingPhong(false);
    }
  };

<<<<<<< HEAD
  // Lấy danh sách khách hàng
  const fetchKhachHangs = async () => {
    setLoadingKhachHang(true);
    try {
      const res = await apiFetch('http://localhost:5189/api/KhachHang?pageNumber=1&pageSize=100');
      const data = await res.json();
      console.log('API Response (fetchKhachHangs):', JSON.stringify(data, null, 2)); // Debug API response
      let list = [];
      if (data && data.data && Array.isArray(data.data.khachHangs)) {
        list = data.data.khachHangs;
      } else if (data && Array.isArray(data.khachHangs)) {
        list = data.khachHangs;
      } else if (data && Array.isArray(data.KhachHangs)) {
        list = data.KhachHangs;
      } else if (Array.isArray(data)) {
        list = data;
      }
      const filtered = list.filter(kh => !kh.trangThai || kh.trangThai.toLowerCase() !== 'vô hiệu hóa');
      setKhachHangs(filtered);
      if (filtered.length === 0) message.warning('Không có khách hàng nào trong hệ thống!');
    } catch (e) {
      console.error('Error fetching khachHangs:', e);
      setKhachHangs([]);
      message.error('Không thể lấy dữ liệu khách hàng!');
    } finally {
      setLoadingKhachHang(false);
    }
  };

  useEffect(() => {
    fetchDatPhongs();
    fetchPhongs();
    fetchKhachHangs();
  }, []);

  // Đồng bộ dữ liệu sau khi thực hiện thao tác
  const syncData = async () => {
    await Promise.all([fetchDatPhongs(), fetchPhongs(), fetchKhachHangs()]);
  };

  // Xử lý xóa đặt phòng
  const handleDelete = async (maDatPhong) => {
    try {
      const res = await apiFetch(`http://localhost:5189/api/DatPhong/${maDatPhong}`, { method: 'DELETE' });
      if (!res.ok) throw new Error('Xóa thất bại');
      message.success('Xóa đặt phòng thành công!');
      await syncData(); // Đồng bộ dữ liệu sau khi xóa
    } catch (e) {
      console.error('Error deleting datPhong:', e);
      message.error('Xóa đặt phòng thất bại!');
    }
  };

  // Hiển thị modal thêm/sửa
  const showModal = async (record = null) => {
    setEditingDatPhong(record);
    setIsModalVisible(true);
    setTimeout(() => {
      if (record) {
        form.setFieldsValue({
          ...record,
          ngayNhanPhong: record.ngayNhanPhong ? dayjs(record.ngayNhanPhong) : null,
          ngayTraPhong: record.ngayTraPhong ? dayjs(record.ngayTraPhong) : null,
          maKhList: record.danhSachKhachHang ? record.danhSachKhachHang.map(kh => kh.maKh) : [],
        });
      } else {
        form.resetFields();
      }
    }, 0);
  };

  // Xử lý thêm/sửa đặt phòng
  const handleOk = async (values) => {
    try {
      if (!values.maPhong) throw new Error('Vui lòng chọn phòng!');
      if (!values.maKh) throw new Error('Vui lòng chọn khách hàng đại diện!');
      if (!values.ngayNhanPhong || !values.ngayTraPhong) throw new Error('Vui lòng chọn đầy đủ ngày nhận và ngày trả phòng!');
      if (dayjs(values.ngayNhanPhong).isAfter(dayjs(values.ngayTraPhong), 'minute')) {
        throw new Error('Ngày nhận phòng phải trước hoặc bằng ngày trả phòng!');
      }
      if (!values.soNguoiO || values.soNguoiO < 1) throw new Error('Số người ở phải lớn hơn 0!');

      // Tự động thêm khách hàng đại diện vào danh sách nếu chưa có
      let maKhList = values.maKhList || [];
      if (!maKhList.includes(values.maKh)) {
        maKhList = [values.maKh, ...maKhList];
      }

      // Kiểm tra số lượng khách hàng so với số người ở
      if (maKhList.length > values.soNguoiO) {
        throw new Error(`Số lượng khách hàng (${maKhList.length}) không được vượt quá số người ở (${values.soNguoiO})!`);
      }

      const updatedValues = {
        ...values,
        maKhList: maKhList,
      };

      if (editingDatPhong) {
        // Sửa đặt phòng
        const res = await apiFetch(`http://localhost:5189/api/DatPhong/${editingDatPhong.maDatPhong}`, {
          method: 'PUT',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify({
            ...updatedValues,
            maKhList: updatedValues.maKhList,
            ngayNhanPhong: dayjs(updatedValues.ngayNhanPhong).format('YYYY-MM-DD HH:mm'),
            ngayTraPhong: dayjs(updatedValues.ngayTraPhong).format('YYYY-MM-DD HH:mm'),
          }),
        });
        if (!res.ok) {
          const err = await res.json().catch(() => ({}));
          throw new Error(err.message || 'Cập nhật thất bại');
        }
        message.success('Cập nhật thành công!');
      } else {
        // Thêm mới đặt phòng
        const payload = {
          datPhong: {
            MaNv: null,
            MaKh: updatedValues.maKh ? Number(updatedValues.maKh) : null,
            MaPhong: updatedValues.maPhong ? String(updatedValues.maPhong) : null,
            NgayDat: dayjs().format('YYYY-MM-DD'),
            NgayNhanPhong: dayjs(updatedValues.ngayNhanPhong).toISOString(),
            NgayTraPhong: dayjs(updatedValues.ngayTraPhong).toISOString(),
            SoNguoiO: updatedValues.soNguoiO ? Number(updatedValues.soNguoiO) : 1,
            TrangThai: updatedValues.trangThai || 'Đã đặt'
          },
          maKhList: maKhList.map(Number),
          DatPhongVMs: [{
            MaKh: updatedValues.maKh ? Number(updatedValues.maKh) : null,
            MaPhong: updatedValues.maPhong ? String(updatedValues.maPhong) : null,
            NgayNhanPhong: dayjs(updatedValues.ngayNhanPhong).toISOString(),
            NgayTraPhong: dayjs(updatedValues.ngayTraPhong).toISOString(),
            SoNguoiO: updatedValues.soNguoiO ? Number(updatedValues.soNguoiO) : 1,
            TrangThai: updatedValues.trangThai || 'Đã đặt'
          }]
        };

        console.log('Payload gửi đi:', JSON.stringify(payload, null, 2));

=======
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
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
        const res = await apiFetch('http://localhost:5189/api/DatPhong', {
          method: 'POST',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify(payload)
        });
<<<<<<< HEAD

        if (!res.ok) {
          const errorData = await res.json();
          console.error('API Error details:', {
            status: res.status,
            statusText: res.statusText,
            data: errorData
          });
          throw new Error(
            errorData.errors ? 
              Object.values(errorData.errors).flat().join(', ') : 
              errorData.message || errorData.title || 'Thêm mới thất bại'
          );
        }
        message.success('Thêm mới thành công!');

        // Sau khi thêm mới, thêm dữ liệu vào danh sách datPhongs
        if (!editingDatPhong) {
          const newDatPhong = {
            ...payload.datPhong,
            maDatPhong: (await res.json()).maDatPhong, // Lấy mã đặt phòng từ API trả về
            danhSachKhachHang: khachHangs.filter(kh => payload.maKhList.includes(kh.maKh)),
          };
          setDatPhongs(prev => [newDatPhong, ...prev]);
        }
=======
        if (!res.ok) throw new Error('Thêm mới thất bại');
        message.success('Thêm mới thành công!');
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
      }
      setIsModalVisible(false);
      setEditingDatPhong(null);
      form.resetFields();
<<<<<<< HEAD
      await syncData(); // Đồng bộ dữ liệu sau khi thêm/sửa
    } catch (e) {
      console.error('Error in handleOk:', e);
=======
      fetchDatPhongs();
    } catch (e) {
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
      message.error(e.message || 'Có lỗi xảy ra!');
    }
  };

<<<<<<< HEAD
  const columns = [
    { title: 'Mã đặt phòng', dataIndex: 'maDatPhong', key: 'maDatPhong' },
    {
      title: 'Khách hàng',
      key: 'khachHang',
      render: (_, record) =>
        record.danhSachKhachHang && record.danhSachKhachHang.length > 0
          ? record.danhSachKhachHang[0].hoTen
          : '',
    },
=======
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
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
    { title: 'Phòng', dataIndex: 'maPhong', key: 'maPhong' },
    { title: 'Ngày nhận', dataIndex: 'ngayNhanPhong', key: 'ngayNhanPhong' },
    { title: 'Ngày trả', dataIndex: 'ngayTraPhong', key: 'ngayTraPhong' },
    { title: 'Trạng thái', dataIndex: 'trangThai', key: 'trangThai' },
<<<<<<< HEAD
    { title: 'Phụ thu', dataIndex: 'phuThu', key: 'phuThu', render: (value) => (value != null ? value : 0) },
=======
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
    {
      title: 'Hành động',
      key: 'action',
      render: (_, record) => (
        <Space>
<<<<<<< HEAD
          <Button onClick={() => showModal(record)}>Sửa</Button>
=======
          <Button onClick={() => showEditModal(record)}>Sửa</Button>
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
          <Popconfirm title="Bạn chắc chắn xóa?" onConfirm={() => handleDelete(record.maDatPhong)}>
            <Button danger>Xóa</Button>
          </Popconfirm>
        </Space>
<<<<<<< HEAD
      ),
    },
  ];

  return (
    <div style={{ padding: 24 }}>
      <h2>Quản lý đặt phòng</h2>
      <Space style={{ marginBottom: 16 }}>
        <Button type="primary" onClick={() => showModal(null)}>
          Thêm đặt phòng
        </Button>
=======
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
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
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
<<<<<<< HEAD
        destroyOnHidden // Thay destroyOnClose bằng destroyOnHidden
      >
        <Spin spinning={loadingPhong || loadingKhachHang}>
          <Form
            form={form} // Thêm prop form để kết nối với useForm
            layout="vertical"
            onFinish={handleOk}
            onFinishFailed={({ errorFields }) => {
              if (errorFields && errorFields.length > 0) {
                message.error(errorFields[0].errors[0] || 'Vui lòng điền đầy đủ thông tin!');
              }
            }}
          >
            <Form.Item label="Mã phòng" name="maPhong" rules={[{ required: true, message: 'Chọn phòng!' }]}>
              <Select
                placeholder={phongs.length === 0 ? 'Không có phòng' : 'Chọn phòng'}
=======
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
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
                loading={loadingPhong}
                showSearch
                optionFilterProp="children"
                allowClear
              >
<<<<<<< HEAD
                {phongs.map((phong) => (
                  <Select.Option key={phong.maPhong} value={phong.maPhong}>
                    {phong.tenPhong} ({phong.maPhong})
                  </Select.Option>
                ))}
              </Select>
            </Form.Item>
            <Form.Item
              label="Khách hàng đại diện"
              name="maKh"
              rules={[{ required: true, message: 'Chọn khách hàng!' }]}
            >
=======
                {phongs.map(phong => (
                  <Select.Option key={phong.maPhong} value={phong.maPhong}>{phong.tenPhong} ({phong.maPhong})</Select.Option>
                ))}
              </Select>
            </Form.Item>
            <Form.Item label="Tên khách hàng" name="tenKhachHang" rules={[{ required: true, message: 'Chọn tên khách hàng!' }]}> 
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
              <Select
                placeholder="Chọn khách hàng"
                loading={loadingKhachHang}
                showSearch
                optionFilterProp="children"
                allowClear
              >
<<<<<<< HEAD
                {khachHangs.map((kh) => (
                  <Select.Option key={kh.maKh} value={kh.maKh}>
                    {kh.hoTen}
                  </Select.Option>
                ))}
              </Select>
            </Form.Item>
            <Form.Item
              label="Ngày nhận phòng"
              name="ngayNhanPhong"
              rules={[{ required: true, message: 'Chọn ngày nhận!' }]}
            >
              <DatePicker style={{ width: '100%' }} showTime format="YYYY-MM-DD HH:mm" />
            </Form.Item>
            <Form.Item
              label="Ngày trả phòng"
              name="ngayTraPhong"
              rules={[{ required: true, message: 'Chọn ngày trả!' }]}
            >
              <DatePicker style={{ width: '100%' }} showTime format="YYYY-MM-DD HH:mm" />
            </Form.Item>
            <Form.Item
              label="Số người ở"
              name="soNguoiO"
              rules={[{ required: true, type: 'number', min: 1, message: 'Nhập số người ở (>=1)!' }]}
            >
              <InputNumber min={1} style={{ width: '100%' }} />
            </Form.Item>
            <Form.Item
              label="Khách hàng (nhiều)"
              name="maKhList"
              extra="Chọn thêm khách hàng nếu có. Khách hàng đại diện sẽ tự động được thêm vào danh sách."
              rules={[
                ({ getFieldValue }) => ({
                  validator(_, value) {
                    const soNguoiO = getFieldValue('soNguoiO') || 1;
                    const maKh = getFieldValue('maKh');
                    const totalKhachHang = (value || []).length + (maKh ? 1 : 0);
                    
                    if (totalKhachHang > soNguoiO) {
                      return Promise.reject(new Error(`Số lượng khách hàng không được vượt quá ${soNguoiO} người!`));
                    }
                    return Promise.resolve();
                  },
                }),
              ]}
            >
              <Select
                mode="multiple"
                placeholder="Chọn thêm khách hàng (nếu có)"
                loading={loadingKhachHang}
                optionFilterProp="children"
                showSearch
                allowClear
              >
                {khachHangs.map((kh) => (
                  <Select.Option key={kh.maKh} value={kh.maKh}>
                    {kh.hoTen}
                  </Select.Option>
                ))}
              </Select>
            </Form.Item>
            <Form.Item label="Trạng thái" name="trangThai">
=======
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
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
              <Select placeholder="Chọn trạng thái">
                <Select.Option value="Đang sử dụng">Đang sử dụng</Select.Option>
                <Select.Option value="Hủy">Hủy</Select.Option>
                <Select.Option value="Hoàn thành">Hoàn thành</Select.Option>
                <Select.Option value="Đã đặt">Đã đặt</Select.Option>
              </Select>
            </Form.Item>
<<<<<<< HEAD
            <Form.Item>
              <Button type="primary" htmlType="submit" onClick={() => form.submit()}>
                {editingDatPhong ? 'Lưu' : 'Thêm đặt phòng'}
              </Button>
            </Form.Item>
=======
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
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
          </Form>
        </Spin>
      </Modal>
    </div>
  );
}

<<<<<<< HEAD
export default DatPhong;
=======
export default DatPhong;
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
