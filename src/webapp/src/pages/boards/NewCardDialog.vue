<template>
  <div class="new-card-dialog">
    <q-dialog ref="dialogRef" @hide="onDialogHide">
      <q-card class="container">
        <form @submit.prevent.stop="onSubmit">
          <q-card-section>
            <span class="text-h6">Add Card</span>
            <q-input v-model="model.name" label="Name" autofocus />
            <label>
              <div class="q-field__label q-mt-md">Description</div>
              <q-editor
                v-model="model.description"
                max-height="5rem"
                min-height="5rem"
              />
            </label>
            <q-select
              v-model="model.ownersId"
              :options="ownerOptions"
              label="Owners"
              multiple
              emit-value
              map-options
            />
            <div class="row q-col-gutter-md">
              <div class="col-8">
                <q-select
                  v-model="model.priority"
                  :options="priorityOptions"
                  label="Priority"
                  emit-value
                  map-options
                />
              </div>
              <div class="col-4">
                <q-input
                  label="Estimated points"
                  v-model.number="model.estimatedPoints"
                  input-class="text-right"
                />
              </div>
            </div>
          </q-card-section>
          <q-card-actions align="right">
            <q-btn label="Save" type="submit" color="primary" />
            <q-btn
              @click="onDialogCancel"
              label="Cancel"
              color="primary"
              outline
            />
          </q-card-actions>
        </form>
      </q-card>
    </q-dialog>
  </div>
</template>
<script setup lang="ts">
import { reactive, ref, onMounted } from 'vue';
import { useDialogPluginComponent } from 'quasar';
import { boardsSvc, developersSvc } from 'src/services';

defineEmits([...useDialogPluginComponent.emits]);

const { dialogRef, onDialogHide, onDialogOK, onDialogCancel } =
  useDialogPluginComponent();

const props = defineProps({
  boardId: {
    type: Number,
    required: true,
  },
});

const model = reactive({
  name: '',
  description: '',
  ownersId: [],
  priority: 1,
  estimatedPoints: 3,
});

const priorityOptions = [
  { value: 0, label: 'Low' },
  { value: 1, label: 'Medium' },
  { value: 2, label: 'High' },
];

const ownerOptions = ref<Array<{ value: number; label: string }>>([]);

onMounted(async () => {
  const devs = (await developersSvc.list()).data;
  ownerOptions.value.push(...devs.map((d) => ({ value: d.id, label: d.name })));
});

const onSubmit = async () => {
  await boardsSvc.registerItem(props.boardId, model);

  onDialogOK();
};
</script>
<style lang="scss">
.new-card-dialog {
  .q-card.container {
    width: 600px;
    max-width: 600px;
  }
}
</style>
