<template>
  <div class="new-card-dialog">
    <q-dialog ref="dialogRef" @hide="onDialogHide">
      <q-card class="container">
        <Form
          :validation-schema="validationSchema"
          :initial-values="defaultValues"
          @submit="onSubmit"
          autocomplete="off"
        >
          <q-card-section>
            <span class="text-h6">Add Card</span>
            <Field name="name" v-slot="{ errorMessage, value, field }">
              <q-input
                label="Name"
                :model-value="value"
                v-bind="field"
                :error-message="errorMessage"
                :error="!!errorMessage"
                autofocus
              />
            </Field>
            <label>
              <div class="q-field__label q-mt-sm">Description</div>
              <q-editor
                v-model="description"
                max-height="8rem"
                min-height="8rem"
              />
            </label>
            <Field name="ownersId" v-slot="{ value, field }">
              <q-select
                label="Owners"
                :options="ownerOptions"
                :model-value="value"
                v-bind="field"
                multiple
                emit-value
                map-options
              />
            </Field>
            <div class="row q-col-gutter-md">
              <div class="col-8">
                <Field name="priority" v-slot="{ value, field }">
                  <q-select
                    label="Priority"
                    :options="priorityOptions"
                    :model-value="value"
                    v-bind="field"
                    emit-value
                    map-options
                  />
                </Field>
              </div>
              <div class="col-4">
                <Field
                  name="estimatedPoints"
                  v-slot="{ errorMessage, value, field }"
                >
                  <q-input
                    label="Estimated points"
                    input-class="text-right"
                    :model-value="value"
                    v-bind="field"
                    :error-message="errorMessage"
                    :error="!!errorMessage"
                    autofocus
                  />
                </Field>
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
        </Form>
      </q-card>
    </q-dialog>
  </div>
</template>
<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { Form, Field } from 'vee-validate';
import * as yup from 'yup';
import { merge } from 'lodash';
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

const description = ref('');

const validationSchema = yup.object({
  name: yup.string().required().label('Name'),
  ownersId: yup.array().label('Owners'),
  priority: yup.number().min(0).max(2).label('Priority'),
  estimatedPoints: yup.number().required().label('Estimated points'),
});

const defaultValues = {
  ownersId: [],
  priority: 1,
  estimatedPoints: 3,
};

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

const onSubmit = async (values: any) => {
  const model = merge(values, { description: description.value });
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
