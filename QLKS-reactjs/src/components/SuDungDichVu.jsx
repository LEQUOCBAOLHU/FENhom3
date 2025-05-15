import React, { useEffect, useState } from 'react';
import './SuDungDichVu.css';

const SuDungDichVu = () => {
  const [data, setData] = useState([]);
  useEffect(() => {
    const token = localStorage.getItem('token');
    fetch('http://localhost:5189/api/SuDungDichVu/get-all', {
      headers: { Authorization: `Bearer ${token}` }
    })
      .then(res => res.json())
      .then(res => Array.isArray(res) ? setData(res) : setData([]))
      .catch(console.error);
  }, []);
  return (
    <div>
      <h2>Danh sách Sử dụng dịch vụ</h2>
      <table className="table-sddv">
        <thead>
          <tr>
            <th>Mã sử dụng</th>
            <th>Mã đặt phòng</th>
            <th>Mã dịch vụ</th>
            <th>Tên dịch vụ</th>
            <th>Số lượng</th>
            <th>Ngày sử dụng</th>
            <th>Ngày kết thúc</th>
            <th>Thành tiền</th>
          </tr>
        </thead>
        <tbody>
          {data.map(sddv => (
            <tr key={sddv.MaSuDung}>
              <td>{sddv.MaSuDung}</td>
              <td>{sddv.MaDatPhong}</td>
              <td>{sddv.MaDichVu}</td>
              <td>{sddv.TenDichVu}</td>
              <td>{sddv.SoLuong}</td>
              <td>{sddv.NgaySuDung ? new Date(sddv.NgaySuDung).toLocaleDateString() : ''}</td>
              <td>{sddv.NgayKetThuc ? new Date(sddv.NgayKetThuc).toLocaleDateString() : ''}</td>
              <td>{sddv.ThanhTien}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default SuDungDichVu;
