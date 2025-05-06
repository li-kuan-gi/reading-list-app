# Reading List App

一個簡單的 Web 應用程式，用於記錄和排序想讀的書籍，幫助使用者管理他們的閱讀清單。

## MVP 功能

* **新增想讀書籍：** 記錄書名和作者。
* **查看想讀書籍列表：** 顯示所有已記錄的想讀書籍。
* **排序想讀書籍：** 允許使用者手動調整列表中的書籍順序。
* **刪除想讀書籍。**

## 技術棧

* **後端：** ASP.NET Core
* **前端：** Vue.js
* **資料庫：** PostgreSQL (預計使用，MVP 可能先使用記憶體儲存)
* **API 規格：** OpenAPI (`openapi.yaml`)

## 本地開發設定

1.  **安裝 .NET Core SDK：** 請確保你的開發環境已安裝 ASP.NET Core SDK。
2.  **安裝 Node.js 和 Vue CLI：** 如果你打算開發前端，請確保已安裝 Node.js (`>= 16.0.0`)。
3.  **API 規格文件：** API 的詳細規格定義在根目錄下的 `openapi.yaml` 檔案中。

## Git Workflow

我們採用 Feature Branch Workflow。

1.  從 `main` 分支創建新的 Feature Branch (`feature/<feature-name>`)。
2.  在 Feature Branch 上進行開發。
3.  提交程式碼並推送到遠端倉庫。
4.  發起 Pull Request (PR) 將 Feature Branch 合併回 `main` 分支。
