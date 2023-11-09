<template>
    <div class="heading">StraightUpFax</div>
    <div v-if="message">{{message}}</div>
    <div class="quote"
         v-for="fact in post"
         :key="fact">
        {{fact}}
        <button @click="rmFact(fact)">Remove fact</button>
    </div>
    <div>
        <input v-model="newFact" />
        <button @click="addFact(newFact)">Add fact</button>
    </div>
</template>

<script setup>
    import { ref } from 'vue'

    const message = ref("Loading Fax...");
    const post = ref(null)
    const newFact = ref('')
    const url = 'https://localhost:8080/' //https://192.168.188.162:8080/

    fetchFax()

    async function fetchFax() {
        post.value = null;
        message.value = 'Loading Fax...'
        try {
            /*await fetch('http://192.168.188.162:8080/getAllFax')*/
            await fetch(url + 'getAllFax')
                .then(r => r.json())
                .then(json => {
                    post.value = json;
                    message.value = null;
                    return;
                });
        }
        catch {
            message.value = 'Fax server could not be reached.';
        }
    }

    async function rmFact(fact) {
        try {
            await fetch(url + 'removeFax', { method: 'POST', body: fact })
        }
        catch {
            message.value = 'Error removing fact.';
        }
        await fetchFax()
    }

    async function addFact(fact) {
        try {
            await fetch(url + 'addFax', { method: 'POST', body: fact })
        }
        catch {
            message.value = 'Error adding fact.';
        }
        await fetchFax()
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
.heading {
    border: 2px solid black;
}
</style>
