<template>
  <div class="boards-index">
    <q-btn @click="addBoard" icon="add" label="Add board" />
    <q-separator class="q-my-md" />
    <div class="row q-col-gutter-lg">
      <div class="col-4" v-for="board in model" :key="board.id">
        <q-card>
          <q-card-section>
            <div class="text-h6">
              {{ board.name }}
            </div>
          </q-card-section>
          <q-card-actions align="right">
            <q-btn
              flat
              icon="arrow_forward"
              :to="{ name: 'BoardDetails', params: { id: board.id } }"
            />
          </q-card-actions>
        </q-card>
      </div>
    </div>
  </div>
</template>
<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { useRouter } from 'vue-router';
import { useQuasar } from 'quasar';
import notifier from 'src/common/notifier';
import PromptDialog from 'src/components/PromptDialog.vue';
import { boardsSvc } from 'src/services';
import { Board } from 'src/models';

const router = useRouter();
const $q = useQuasar();

const model = ref<Board[]>([]);

const loadData = async () => {
  model.value = (await boardsSvc.list()).data;
};

const addBoard = () => {
  $q.dialog({
    component: PromptDialog,
    componentProps: {
      message: 'New board name',
      validationMessage: 'Name is required',
    },
  }).onOk(async (result: { value: string }) => {
    const board = (await boardsSvc.register({ name: result.value })).data;

    notifier.success('Board registered.');

    router.push({
      name: 'BoardDetails',
      params: {
        id: board.id,
      },
    });
  });
};

onMounted(async () => {
  await loadData();
});
</script>
