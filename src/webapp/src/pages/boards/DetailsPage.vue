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
        <q-card-section>
          <div v-if="index == 0" class="text-center">
            <q-btn @click="addCard" label="Add card" outline color="primary" />
          </div>
          <div class="text-left">
            <draggable
              v-model="item.items"
              :group="{ name: 'myGroup' }"
              item-key="id"
              title="5"
              @end="cardMoving"
              style="height: 500px"
              :data-column-id="item.id"
            >
              <template #item="{ element }">
                <q-card class="q-mt-md" :data-card-id="element.id">
                  <q-card-section>{{ element.card.name }}</q-card-section>
                </q-card>
              </template>
            </draggable>
          </div>
        </q-card-section>
      </q-card>
    </q-virtual-scroll>
  </div>
</template>
<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { useRoute } from 'vue-router';
import { useQuasar } from 'quasar';
import draggable from 'vuedraggable';
import { boardsSvc } from 'src/services';
import { Board, BoardItem } from 'src/models';
import NewCardDialog from './NewCardDialog.vue';
import notifier from 'src/common/notifier';

const route = useRoute();
const $q = useQuasar();

const model = ref<Board>(new Board());

const columns = ref<
  Array<{ id: number | undefined; name: string; items: Array<BoardItem> }>
>([{ id: undefined, name: 'Backlog', items: [] }]);

const addCard = () => {
  $q.dialog({
    component: NewCardDialog,
    componentProps: {
      boardId: model.value.id,
    },
  }).onOk(async () => {
    notifier.success('Card added');
    await loadItems();
  });
};

const cardMoving = (event: any) => {
  const oldIndex = parseInt(event.oldIndex);

  const newIndex = parseInt(event.newIndex);

  const fromColumnId =
    event.from.getAttribute('data-column-id') == undefined
      ? undefined
      : parseInt(event.from.getAttribute('data-column-id'));

  const toColumnId = parseInt(event.to.getAttribute('data-column-id'));

  const cardId = event.item.getAttribute('data-card-id');
};

const loadItems = async () => {
  columns.value.forEach((col) => {
    col.items = [];
  });

  const items = (await boardsSvc.listItems(model.value.id)).data;
  items.forEach((i) => {
    const col = columns.value.find((c) => c.id == i.columnId);
    col?.items.push(i);
  });
};

onMounted(async () => {
  const id = parseInt(<string>route.params.id);
  model.value = (await boardsSvc.get(id)).data;

  columns.value.push(
    ...model.value.columns.map((c) => ({ id: c.id, name: c.name, items: [] }))
  );

  await loadItems();
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
