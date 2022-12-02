<template>
  <q-layout view="LHh Lpr lFf">
    <q-header>
      <q-toolbar>
        <q-btn
          flat
          dense
          round
          icon="menu"
          aria-label="Menu"
          @click="toggleLeftDrawer"
        />

        <q-toolbar-title>
          <q-btn flat label="Workboard" to="/" />
        </q-toolbar-title>

        <!-- <div>Quasar v{{ $q.version }}</div> -->
      </q-toolbar>
    </q-header>

    <q-drawer v-model="leftDrawerOpen" show-if-above>
      <q-toolbar class="bg-primary text-white"> </q-toolbar>
      <q-scroll-area
        style="
          height: calc(100% - 51px);
          border-right: 1px solid rgba(0, 0, 0, 0.12);
        "
      >
        <q-list
          bordered
          class="text-primary"
          style="border-left: none; border-right: none; border-top: none"
        >
          <q-expansion-item
            expand-separator
            expand-icon-toggle
            default-opened
            label="MENU"
          >
            <q-list>
              <q-item clickable v-ripple active-class="bg-blue-1" to="/boards">
                <q-item-section>Boards</q-item-section>
              </q-item>
            </q-list>
          </q-expansion-item>
        </q-list>
      </q-scroll-area>
    </q-drawer>

    <q-page-container>
      <div
        class="q-pa-sm"
        style="max-width: 1200px; margin-left: auto; margin-right: auto"
      >
        <router-view />
      </div>
    </q-page-container>
  </q-layout>
</template>

<script setup lang="ts">
import { ref } from 'vue';

const leftDrawerOpen = ref(false);

function toggleLeftDrawer() {
  leftDrawerOpen.value = !leftDrawerOpen.value;
}
</script>
