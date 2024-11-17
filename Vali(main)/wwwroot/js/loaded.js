window.addEventListener('DOMContentLoaded', event => {
    const datatablesSimple = document.getElementById('datatablesSimple');
    if (datatablesSimple) {
        new simpleDatatables.DataTable(datatablesSimple);
    }
});
//tạo một đối tượng DataTable mới, biến bảng HTML này thành một bảng tương tác với các tính năng như sắp xếp, tìm kiếm và phân trang, nhờ vào thư viện simple-datatables.