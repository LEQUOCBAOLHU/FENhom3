import React, { useEffect, useState } from 'react';
import { Table, Card, Button, Space, Tag } from 'antd';
import { apiFetch } from '../auth';

function QuanLyPhong() {
  const [phongs, setPhongs] = useState([]);
  const [loading, setLoading] = useState(false);

  const fetchAllRooms = async () => {
    setLoading(true);
    try {
      const res = await apiFetch('http://localhost:5189/api/Phong/get-all');
      const data = await res.json();
      setPhongs(Array.isArray(data) ? data : []);
    } catch (e) {
      setPhongs([]);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => { fetchAllRooms(); }, []);

  // Hiển thị thông báo nếu không có dữ liệu
  const hasData = Array.isArray(phongs) && phongs.length > 0;

  const columns = [
    { title: 'Mã phòng', dataIndex: 'maPhong', key: 'maPhong' },
    { title: 'Tên phòng', dataIndex: 'tenPhong', key: 'tenPhong' },
    { title: 'Trạng thái', dataIndex: 'trangThai', key: 'trangThai', render: (text) => (
      <Tag color={text === 'Đang sử dụng' ? 'orange' : text === 'Đã đặt' ? 'blue' : text === 'Bảo trì' ? 'red' : 'green'}>{text}</Tag>
    ) },
    { title: 'Loại phòng', dataIndex: 'tenLoaiPhong', key: 'tenLoaiPhong' },
  ];

  return (
    <Card title="Quản lý tất cả phòng (Chỉ dành cho Quản lý)" style={{margin:24}}>
      <Space style={{marginBottom:16}}>
        <Button onClick={fetchAllRooms}>Làm mới</Button>
      </Space>
      <Table columns={columns} dataSource={phongs} rowKey="maPhong" loading={loading} locale={{emptyText: hasData ? undefined : 'Không có dữ liệu phòng!'}} />
    </Card>
  );
}

export default QuanLyPhong;
