////Khai báo các biến
//NONCLIENTMETRICS m_nonClientMetrics; 
//LOGFONT m_logFont;

////Khai báo các biến chứa sơ đồ font
//char m_fontCaption[256];
//char m_fontSmCaption[256];
//char m_fontMenu[256];
//char m_fontMessage[256];
//char m_fontStatus[256];
//char m_fontIcon[256];

//// Thủ tụ thiết lập sơ đồ font hệ thống Windows về font chữ mong muốn
//void SetSysFont(char *fontname) {

//// Truy xuất sơ đồ font hệ thống hiện tại
//m_nonClientMetrics.cbSize = sizeof(m_nonClientMetrics);
//SystemParametersInfo(SPI_GETNONCLIENTMETRICS, sizeof(m_nonClientMetrics), (PVOID)&m_nonClientMetrics, 0);
//SystemParametersInfo(SPI_GETICONTITLELOGFONT, sizeof(m_logFont), (PVOID)&m_logFont, 0);

//// Cất lại font dùng hiển thị các Caption
//strcpy(m_fontCaption,m_nonClientMetrics.lfCaptionFont.lfFaceName);

//// Cất lại font dùng hiển thị các Caption nhỏ
//strcpy(m_fontSmCaption,m_nonClientMetrics.lfSmCaptionFont.lfFaceName);

//// Cất lại font dùng hiển thị các Menu
//strcpy(m_fontMenu,m_nonClientMetrics.lfMenuFont.lfFaceName);

//// Cất lại font dùng hiển thị các hộp thoại thông báo
//strcpy(m_fontMessage,m_nonClientMetrics.lfMessageFont.lfFaceName);

//// Cất lại font dùng hiển thị thông tin ở thanh trạng thái và tooltips
//strcpy(m_fontStatus,m_nonClientMetrics.lfStatusFont.lfFaceName);

//// Cất lại font dùng hiển thị tên các Icon chương trình
//strcpy(m_fontIcon,m_logFont.lfFaceName);

//// Thay đổi thành font chữ tiếng Việt
//strcpy(m_nonClientMetrics.lfCaptionFont.lfFaceName,fontname);
//strcpy(m_nonClientMetrics.lfSmCaptionFont.lfFaceName,fontname);
//strcpy(m_nonClientMetrics.lfMenuFont.lfFaceName,fontname);
//strcpy(m_nonClientMetrics.lfMessageFont.lfFaceName,fontname);
//strcpy(m_nonClientMetrics.lfStatusFont.lfFaceName,fontname);

//SystemParametersInfo(SPI_SETNONCLIENTMETRICS, sizeof(m_nonClientMetrics), &m_nonClientMetrics, SPIF_SENDCHANGE|SPIF_UPDATEINIFILE);

//strcpy(m_logFont.lfFaceName,fontname);

//SystemParametersInfo(SPI_SETICONTITLELOGFONT, sizeof(m_logFont), &m_logFont, 
//SPIF_SENDCHANGE|SPIF_UPDATEINIFILE);
//}

//// Phục hồi sơ đồ font cũ của Windows
//void RestoreSysFont() {

////' Phục hồi lại sơ đồ font chữ của Windows trước khi chạy ứng dụng
//strcpy(m_nonClientMetrics.lfCaptionFont.lfFaceName,m_fontCaption);
//strcpy(m_nonClientMetrics.lfSmCaptionFont.lfFaceName,m_fontSmCaption);
//strcpy(m_nonClientMetrics.lfMenuFont.lfFaceName,m_fontMenu);
//strcpy(m_nonClientMetrics.lfMessageFont.lfFaceName,m_fontMessage);
//strcpy(m_nonClientMetrics.lfStatusFont.lfFaceName,m_fontStatus);

//SystemParametersInfo(SPI_SETNONCLIENTMETRICS, sizeof(m_nonClientMetrics), 
//&m_nonClientMetrics, SPIF_SENDCHANGE|SPIF_UPDATEINIFILE);

//strcpy(m_logFont.lfFaceName,m_fontIcon);

//SystemParametersInfo(SPI_SETICONTITLELOGFONT, sizeof(m_logFont), &m_logFont, SPIF_SENDCHANGE|SPIF_UPDATEINIFILE);
//}