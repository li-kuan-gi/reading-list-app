<template>
    <div>
        <h1>想讀書單</h1>
        <div v-if="error" class="alert alert-danger">{{ error }}</div>
        <div v-if="isLoading" class="d-flex justify-content-center mt-3">
            <div class="spinner-border text-primary" role="status">
                <span class="visually-hidden">Loading...</span>
            </div>
        </div>
        <ul v-else-if="books.length > 0" class="list-group">
            <li v-for="book in books" :key="book.id" class="list-group-item">
                {{ book.title }} - {{ book.author }}
            </li>
        </ul>
        <div v-else-if="!isLoading && !error">
            <p>你的想讀書單是空的！</p>
        </div>
    </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';

const books = ref([]);
const error = ref(null);
const isLoading = ref(false);

const reloadBook = async () => {
    isLoading.value = true;
    error.value = null;
    try {
        const response = await fetch('/api/reading-list');
        if (!response.ok) {
            error.value = `Failed to fetch books: ${response.status}`;
            return;
        }
        const data = await response.json();
        books.value = data;
    } catch (err) {
        error.value = 'An unexpected error occurred while fetching books.';
    } finally {
        isLoading.value = false;
    }
}

onMounted(reloadBook);
</script>
