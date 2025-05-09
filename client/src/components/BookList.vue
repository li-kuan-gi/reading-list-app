<template>
    <div>
        <h1>想讀書單</h1>
        <div v-if="loadingError" class="alert alert-danger">{{ loadingError }}</div>
        <div v-if="deleteError" class="alert alert-danger">{{ deleteError }}</div>
        <div v-if="isLoading" class="d-flex justify-content-center mt-3">
            <div class="spinner-border text-primary" role="status">
                <span class="visually-hidden">Loading...</span>
            </div>
        </div>
        <ul v-else-if="books.length > 0" class="list-group">
            <li v-for="book in books" :key="book.id"
                class="list-group-item d-flex justify-content-between align-items-center">
                <span>{{ book.title }} - {{ book.author }}</span>
                <button class="btn btn-danger btn-sm" @click="confirmDeleteBook(book.id)">刪除</button>
            </li>
        </ul>
        <div v-else-if="!isLoading && !loadingError">
            <p>你的想讀書單是空的！</p>
        </div>
        <div v-if="showConfirmationModal" class="modal fade show" style="display: block;">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">確認刪除</h5>
                        <button type="button" class="btn-close" @click="cancelDelete"></button>
                    </div>
                    <div class="modal-body">
                        確定要刪除這本書嗎？
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" @click="cancelDelete">取消</button>
                        <button type="button" class="btn btn-danger" @click="deleteBook">確認刪除</button>
                    </div>
                </div>
            </div>
        </div>
        <div v-if="showConfirmationModal" class="modal-backdrop fade show"></div>
    </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';

const books = ref([]);
const loadingError = ref(null);
const isLoading = ref(false);

const bookToDeleteId = ref(null);
const deleteError = ref(null);
const showConfirmationModal = ref(false);

const reloadBook = async () => {
    isLoading.value = true;
    loadingError.value = null;
    try {
        const response = await fetch('/api/reading-list');
        if (!response.ok) {
            loadingError.value = `Failed to fetch books: ${response.status}`;
            return;
        }
        const data = await response.json();
        books.value = data;
    } catch (err) {
        loadingError.value = 'An unexpected error occurred while fetching books.';
    } finally {
        isLoading.value = false;
    }
}

const confirmDeleteBook = (id) => {
    bookToDeleteId.value = id;
    showConfirmationModal.value = true;
}

const cancelDelete = () => {
    showConfirmationModal.value = false;
    bookToDeleteId.value = null;
}

const deleteBook = async () => {
    if (!bookToDeleteId.value) return;

    deleteError.value = null;
    showConfirmationModal.value = false;

    try {
        const response = await fetch(`/api/reading-list/${bookToDeleteId.value}/delete`, {
            method: 'POST',
        });

        if (response.ok && response.status === 204) {
            // books.value = books.value.filter(book => book.id !== bookToDeleteId.value);
            await reloadBook();
        } else {
            deleteError.value = "刪除書籍時發生意外錯誤。"
        }
    } catch (err) {
        deleteError.value = "刪除書籍時發生意外錯誤。"
    } finally {
        bookToDeleteId.value = null;
    }
}

onMounted(reloadBook);
</script>
