<template>
  <div class="boards-index">
    <q-btn icon="add" label="Add board" />
    <q-separator class="q-my-md" />
    <div class="row q-col-gutter-lg">
      <div class="col-4" v-for="board in model" :key="board.id">
        <q-card class="cursor-pointer">
          <q-card-section>
            <div class="text-h6 text-center">
              {{ board.name }}
            </div>
          </q-card-section>
        </q-card>
      </div>
    </div>
  </div>
</template>
<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { boardsSvc } from 'src/services';
import { Board } from 'src/models';

const model = ref<Board[]>([]);

onMounted(async () => {
  model.value = (await boardsSvc.list()).data;
});
</script>
