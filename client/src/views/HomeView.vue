<script setup>
import BookList from '@/components/BookList.vue';
import AddBookForm from '../components/AddBookForm.vue'

import { ref } from 'vue';

const remountBookList = ref(false);

const didAddBook = () => {
  remountBookList.value = !remountBookList.value;
}
</script>

<template>
  <AddBookForm @did-add-book="didAddBook" />
  <BookList :key="remountBookList" />
</template>
