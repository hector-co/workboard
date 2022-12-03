<template>
  <div class="boards-detail">
    <div class="row">
      <div class="col-auto">
        <q-btn icon="arrow_back" label="Back" to="/boards" />
      </div>
      <div class="col">
        <div class="text-h6 q-ml-md">
          {{ model.name }}
        </div>
      </div>
    </div>
    <q-separator class="q-my-md" />
    <q-virtual-scroll
      :items="columns"
      virtual-scroll-horizontal
      v-slot="{ item, index }"
    >
      <q-card :key="index" flat bordered class="card-column">
        <q-card-section>
          <div class="text-center">{{ item.name }}</div>
        </q-card-section>
        <q-separator />
        <q-card-section></q-card-section>
      </q-card>
    </q-virtual-scroll>
  </div>
</template>
<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { useRoute } from 'vue-router';
import { boardsSvc } from 'src/services';
import { Board } from 'src/models';

const route = useRoute();
const model = ref<Board>(new Board());
const columns = ref<Array<{ id: number; name: string }>>([
  { id: 0, name: 'Backlog' },
]);

onMounted(async () => {
  const id = parseInt(<string>route.params.id);
  model.value = (await boardsSvc.get(id)).data;

  columns.value.push(...model.value.columns);
});
</script>
<style lang="scss">
.boards-detail {
  .card-column {
    width: 250px;
    min-height: 550px;
  }
}
</style>
