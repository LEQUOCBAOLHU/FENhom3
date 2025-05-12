import React, { useEffect, useState } from 'react';
import {
  Table, Button, Modal, Form, Input, Select,
  Typography, Card, Row, Col, Statistic, Space, Tag
} from 'antd';
import { PieChart, Pie, Cell, ResponsiveContainer, Tooltip } from 'recharts';
import { 
  UserOutlined, KeyOutlined, CheckCircleOutlined,
  CloseCircleOutlined, LoadingOutlined 
} from '@ant-design/icons';
import './Phong.css';
import { apiFetch } from '../auth';

const { Title } = Typography;
const { Option } = Select;
const COLORS = ['#00C49F', '#FF8042', '#FFBB28'];

function Phong() {
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [phongs, setPhongs] = useState([]);
  const [roomStats, setRoomStats] = useState({
    available: 0,
    occupied: 0,
    maintenance: 0
  });

  const fetchRooms = async () => {
    try {
      const response = await apiFetch('/api/Phong');
      if (response.ok) {
        const data = await response.json();
        setPhongs(data);
        // Tính toán thống kê phòng
        const stats = {
          available: data.filter(room => room.tinhTrang === 'Trống').length,
          occupied: data.filter(room => room.tinhTrang === 'Đã đặt').length,
          maintenance: data.filter(room => room.tinhTrang === 'Bảo trì').length
        };
        setRoomStats(stats);
      }
    } catch (error) {
      console.error('Error fetching rooms:', error);
    }
  };

  useEffect(() => {
    fetchRooms();
  }, []);

  const pieChartData = [
    { name: 'Phòng trống', value: roomStats.available },
    { name: 'Phòng đã đặt', value: roomStats.occupied },
    { name: 'Đang bảo trì', value: roomStats.maintenance }
  ];

  const columns = [
    {
      title: 'Mã phòng',
      dataIndex: 'maPhong',
      key: 'maPhong',
      sorter: (a, b) => a.maPhong.localeCompare(b.maPhong),
      // ...other column properties
    },
    {
      title: 'Tên phòng',
      dataIndex: 'tenPhong',
      key: 'tenPhong',
      sorter: (a, b) => a.tenPhong.localeCompare(b.tenPhong),
      // ...other column properties
    },
    {
      title: 'Trạng thái',
      dataIndex: 'trangThai',
      key: 'trangThai',
      filters: [
        { text: 'Trống', value: 'Trống' },
        { text: 'Đang sử dụng', value: 'Đang sử dụng' },
        { text: 'Bảo trì', value: 'Bảo trì' },
      ],
      onFilter: (value, record) => record.trangThai.includes(value),
      render: (text) => (
        <Tag color={text === 'Trống' ? 'green' : text === 'Đang sử dụng' ? 'orange' : 'red'}>
          {text}
        </Tag>
      ),
    },
    {
      title: 'Loại phòng',
      dataIndex: 'tenLoaiPhong',
      key: 'tenLoaiPhong',
      sorter: (a, b) => a.tenLoaiPhong.localeCompare(b.tenLoaiPhong),
      // ...other column properties
    },
  ];

  return (
    <div className="phong-container">
      <Title level={2} className="page-title">Quản lý Phòng</Title>

      {/* Dashboard Section */}
      <Row gutter={[16, 16]} className="stats-section">
        <Col xs={24} sm={12} md={6}>
          <Card className="stat-card">
            <Statistic
              title="Tổng số phòng"
              value={roomStats.total}
              prefix={<UserOutlined />}
            />
          </Card>
        </Col>
        <Col xs={24} sm={12} md={6}>
          <Card className="stat-card">
            <Statistic
              title="Phòng trống"
              value={roomStats.available}
              prefix={<KeyOutlined />}
              valueStyle={{ color: '#00C49F' }}
            />
          </Card>
        </Col>
        <Col xs={24} sm={12} md={6}>
          <Card className="stat-card">
            <Statistic
              title="Phòng đã đặt"
              value={roomStats.occupied}
              prefix={<CheckCircleOutlined />}
              valueStyle={{ color: '#FF8042' }}
            />
          </Card>
        </Col>
        <Col xs={24} sm={12} md={6}>
          <Card className="stat-card">
            <Statistic
              title="Đang bảo trì"
              value={roomStats.maintenance}
              prefix={<CloseCircleOutlined />}
              valueStyle={{ color: '#FFBB28' }}
            />
          </Card>
        </Col>
      </Row>

      {/* Chart Section */}
      <Row gutter={[16, 16]} className="chart-section">
        <Col xs={24} md={12}>
          <Card className="chart-card">
            <Title level={4}>Trạng thái phòng</Title>
            <ResponsiveContainer width="100%" height={300}>
              <PieChart>
                <Pie
                  data={pieChartData}
                  cx="50%"
                  cy="50%"
                  labelLine={false}
                  label={({ name, percent }) => `${name}: ${(percent * 100).toFixed(0)}%`}
                  outerRadius={100}
                  fill="#8884d8"
                  dataKey="value"
                >
                  {pieChartData.map((entry, index) => (
                    <Cell key={`cell-${index}`} fill={COLORS[index % COLORS.length]} />
                  ))}
                </Pie>
                <Tooltip />
              </PieChart>
            </ResponsiveContainer>
          </Card>
        </Col>
        <Col xs={24} md={12}>
          <Card className="chart-card">
            <Space className="action-buttons">
              <Button type="primary" onClick={() => setIsModalVisible(true)}>
                Thêm phòng mới
              </Button>
              <Button onClick={fetchRooms}>Làm mới dữ liệu</Button>
            </Space>
          </Card>
        </Col>
      </Row>

      {/* Room Table */}
      <Card className="table-card">
        <Table
          columns={columns}
          dataSource={phongs}
          rowKey="maPhong"
          pagination={{
            pageSize: 10,
            showTotal: (total, range) => `${range[0]}-${range[1]} of ${total} phòng`
          }}
        />
      </Card>

      {/* ... existing modals ... */}
    </div>
  );
}

export default Phong;
