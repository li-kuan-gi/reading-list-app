<template>
  <div>
    <div v-if="error" class="alert alert-danger">{{ error }}</div>
    <div v-if="successMessage" class="alert alert-success">{{ successMessage }}</div>
    <div v-if="isLoading" class="d-flex justify-content-center mt-3">
      <div class="spinner-border text-primary" role="status">
        <span class="visually-hidden">Loading...</span>
      </div>
    </div>


  <div class="container mt-5">
    <form @submit.prevent="addBook">
      <div class="mb-3">
        <label for="title" class="form-label">Title</label>
        <input
          type="text"
          class="form-control"
          id="title"
          v-model="book.title"
          required
        />
       
      </div>
      <div class="mb-3">
        <label for="author" class="form-label">Author</label>
        <input
          type="text"
          class="form-control"
          id="author"
          v-model="book.author"
          required
        />
      </div>
      <button type="submit" class="btn btn-primary" :disabled="isLoading">
        Add Book
      </button>
    </form>
  </div>
    </div>
</template>


<script setup>
import { ref } from 'vue';

const error = ref(null);
const book = ref({
  title: '',
  author: '',
});
const isLoading = ref(false);
const successMessage = ref(null);

const addBook = async () => {
  isLoading.value = true;
  error.value = null;
  try {
    const response = await fetch('/api/reading-list/add', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        title: book.value.title,
        author: book.value.author,
      }),
    });

    if (!response.ok) {
      if (response.status === 400 || response.status === 500) {
        error.value = "An unexpected error occurred. Please try again later.";
      } else {
        error.value = `An error has occured: ${response.status}`;
      }
      return;
    }
    
    // Assuming the API returns the added book data
    const data = await response.json();
    console.log('Book added:', data);
    // Reset the form
    book.value = { title: '', author: '' };    
    successMessage.value = 'Book added successfully!';
    
  } catch (error) {
    error.value = "An unexpected error occurred. Please try again later.";
  }finally{
    isLoading.value = false;
  }
};

</script>
