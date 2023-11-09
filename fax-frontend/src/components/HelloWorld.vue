<template>
    <div class="heading">StraightUpFax</div>
    <div v-if="loading">Loading Fax...</div>
    <div class="quote"
         v-for="fact in post"
         :key="fact">
        <p>{{fact}}</p>
    </div>
</template>

<script setup>
    import { ref } from 'vue'

    const loading = ref(true);
    const post = ref(null)

    fetchFax()

    async function fetchFax() {
        post.value = null;
        loading.value = true;

        /*await fetch('http://192.168.188.162:8080/getAllFax')*/
        await fetch('https://localhost:8080/getAllFax')
            .then(r => r.json())
            .then(json => {
                post.value = json;
                loading.value = false;
                return;
            });
    }
</script>

<style scoped>
h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>
