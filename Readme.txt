Xây dựng website giới thiệu công ty :

Công nghệ sử dụng  : Asp.net MVC -MSSQL Server - Linq - Ajax-Jquery

Code First - Database First - Model First (Tùy chọn).

Design pattern (Tùy chọn) 

Yêu cầu admin : 

+ Moudule đăng nhập (đăng nhập * không cần đăng ký) :
 - VD : UserName - Name - Email - Password - CreateDate - IsAdmin - IsEditor
 Mô tả : User Admin có thể đăng nhập vào và quản trị toàn bộ hệ thống & quản lí các user khác - Thêm user mới set quyền admin hoặc editor . 
 User editor chỉ có thể thêm - cập nhật - xóa bài viết , không được đc sử dụng các module khác , phân trang dữ liệu .
(phiên đăng nhập của user tồn tại tới khi bấm logout).

+ Module thông tin công ty chứa các thông tin :
 - NameCompany- Logo - Tel - Hotline - Email - Address - Terms conditions - Description - Metatitle - MetaDescription
 Mô tả : Nếu chưa có bản ghi thông tin công ty -> có thể thêm mới. Nhưng không thể thêm mới hoặc xóa khi đã có bản ghi thông tin công ty -> chỉ được sửa.

+ Moudule chuyên mục menu *yêu cầu có phân cấp menu (cha-con , đầu trang - chân trang) :
- VD Fields : NameMenu - Location - ParentId - Level - Status - IsActive - Description - Metatitle - MetaDescription

+ Module bài viết (các viết về công ty - có liên kết tới bảng menu để chọn chuyên mục bài viết - phân trang dữ liệu).
- VD Fields : NameArticle - Image- MenuId - CreateDate - Content - Status - IsActive - ShowHome - Description - Metatitle - MetaDescription

= Module quản lý liên hệ : 
 - Hiển thị tất cả các liên hệ mà client gửi (Hiển thị và xóa - không thêm mới , phân trang dữ liệu)
- VD Fields : Name  - Email - Phone - Address - Request- CreateDate

Yêu cầu client : 
+ Trang chủ :
 - Yêu cầu hiển thị dữ liệu như template .

+ Trang danh sách bài viết : 
 - Hiển thị danh sách bài viết , có phân trang .

+ Trang chi tiết bài viết
 - Hiển thị nội dung của bài viết đó + các bài viết liên quan nằm trong chuyên mục đó .

- Trang liên hệ :
- Nhận thông tin form liên hệ từ client và lưu vào bảng quản lí liên hệ trong database - Nếu không có lỗi -> Gửi email thông báo cho admin và user .
*Có validate và captcha


*Module gửi email bắt buộc .
Mail thông báo cho admin : Chứa thông tin mà client đã điền vào form và ngày tháng client gửi form.
Mail thông báo cho user : Thông báo email đã được gửi thành công 