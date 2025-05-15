import React, { useEffect, useState } from 'react';
import './LoaiPhong.css';

const LoaiPhong = () => {
  const [data, setData] = useState([]);
  useEffect(() => {
    const token = localStorage.getItem('token');
    fetch('http://localhost:5189/api/LoaiPhong/GetAll', {
      headers: { Authorization: `Bearer ${token}` }
    })
      .then(res => res.json())
      .then(res => Array.isArray(res) ? setData(res) : setData([]))
      .catch(console.error);
  }, []);
  return (
    <div>
      <h2>Danh sách Loại phòng</h2>
      <table className="table-loaiphong">
        <thead>
          <tr>
            <th>Mã loại phòng</th>
            <th>Tên loại phòng</th>
            <th>Giá cơ bản</th>
            <th>Số người tối đa</th>
          </tr>
        </thead>
        <tbody>
          {data.map(lp => (
            <tr key={lp.MaLoaiPhong}>
              <td>{lp.MaLoaiPhong}</td>
              <td>{lp.TenLoaiPhong}</td>
              <td>{lp.GiaCoBan}</td>
              <td>{lp.SoNguoiToiDa}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default LoaiPhong;
