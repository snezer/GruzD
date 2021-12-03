<template>
  <div>
    <DataTable :value="providers">
      <Column field="Name" header="Наименование"></Column>
      <Column field="Phone" header="Номер телефона"></Column>
      <Column field="Email" header="Email"></Column>
      <Column field="INN" header="ИНН"></Column>
    </DataTable>
  </div>
</template>

<script lang="ts">
import {Component, Vue} from "vue-property-decorator";
@Component({})
export default class Providers extends Vue{
  get providers(){
    return this.$store.state.providers.providers
  }

  created(){
    this.$store.dispatch('providers/get_providers')
  }
};
</script>

<style scoped>

</style>
