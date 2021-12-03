<template>
  <div>
    <DataTable :value="deliverys">
      <Column field="ProviderName" header="Наименование поставщика"></Column>
      <Column field="RawMaterialName" header="Материал поставки"></Column>
      <Column field="PlannedDate" header="Плановая дата поставки">
<!--        <template #body="slotProps">-->
<!--          {{new Date(slotProps.PlannedDate).toLocaleDateString('ru-RU', {year: "numeric", month: "2-digit", day: "2-digit"})}}-->
<!--        </template>-->
      </Column>
      <Column field="FactDate" header="Фактическая дата поставки">
<!--        <template #body="slotProps">-->
<!--          {{new Date(slotProps.FactDate).toLocaleDateString('ru-RU', {year: "numeric", month: "2-digit", day: "2-digit"})}}-->
<!--        </template>-->
      </Column>
    </DataTable>
  </div>
</template>

<script lang="ts">
import {Component, Vue} from "vue-property-decorator";
@Component({})
export default class Delivery extends Vue{
  get deliverys(){
    return this.$store.state.delivery.deliverys
  }

  created(){
    this.$store.dispatch('delivery/get_deliverys')
  }
};
</script>

<style scoped>

</style>
