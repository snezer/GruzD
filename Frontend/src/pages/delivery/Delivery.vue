<template>
  <div>
    <DataTable :value="deliverys">
      <Column field="ProviderName" header="Наименование поставщика"></Column>
      <Column field="RawMaterialName" header="Материал поставки"></Column>
      <Column field="PlannedDate" header="Плановая дата поставки">
        <template #body="slotProps">
          {{calcDate(slotProps.data.PlannedDate)}}
        </template>
      </Column>
      <Column field="FactDate" header="Фактическая дата поставки">
        <template #body="slotProps">
          {{calcDate(slotProps.data.FactDate)}}
        </template>
      </Column>
    </DataTable>
  </div>
</template>

<script lang="ts">
import {Component, Vue} from "vue-property-decorator";
@Component({})
export default class Delivery extends Vue{
  calcDate(date){
    return new Date(date).toLocaleDateString('ru-Ru', {year: "numeric", month: "2-digit", day: "2-digit", hour: "2-digit", minute: "2-digit", second: "2-digit"})
  }
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
