<template>
  <div class="prompt-dialog">
    <q-dialog ref="dialogRef" @hide="onDialogHide">
      <q-card class="q-dialog-plugin">
        <form @submit.prevent.stop="onSubmit">
          <q-card-section>
            <q-input
              ref="valueRef"
              v-model="value"
              :label="message"
              autofocus
              :rules="valueRule"
              lazy-rules
            />
          </q-card-section>
          <q-card-actions align="right">
            <q-btn :label="okLabel" type="submit" color="primary" />
            <q-btn
              @click="onDialogCancel"
              :label="cancelLabel"
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
import { ref, unref } from 'vue';
import { useDialogPluginComponent } from 'quasar';

const props = defineProps({
  message: {
    type: String,
    default: 'Input value',
  },
  defaultValue: {
    type: String,
    default: '',
  },
  okLabel: {
    type: String,
    default: 'Ok',
  },
  cancelLabel: {
    type: String,
    default: 'Cancel',
  },
});

defineEmits([...useDialogPluginComponent.emits]);

const { dialogRef, onDialogHide, onDialogOK, onDialogCancel } =
  useDialogPluginComponent();

const value = ref(props.defaultValue);
const valueRule = [(val: string) => (val && val.length > 0) || 'Specify value'];
const valueRef = ref<any>(null);

const onSubmit = () => {
  valueRef.value.validate();

  if (valueRef.value.hasError) return;

  console.log('ok');
};

function onOKClick() {
  onDialogOK({ value: unref(value) });
}
</script>
<style lang="scss">
.prompt-dialog {
  .q-card.container {
    width: 450px;
    max-width: 450px;
  }
}
</style>
