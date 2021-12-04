<template>
  <div>
    <DataTable :value="deliverys">
      <Column field="ProviderId" header="Наименование поставщика">
        <template #body="slotProps">
          {{getNameProvider(slotProps.data.ProviderId)}}
        </template>
      </Column>
      <Column field="RawMaterialName" header="Материал поставки">
        <template #body>
          Гречка высшего сорта
        </template>
      </Column>
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
  getNameProvider(ProviderId){
    return this.$store.state.providers.providers.find(pr=>pr.Id==ProviderId).Name
  }
  calcDate(date){
    return new Date(date).toLocaleDateString('ru-Ru', {year: "numeric", month: "2-digit", day: "2-digit", hour: "2-digit", minute: "2-digit", second: "2-digit"})
  }
  get deliverys(){
    return this.$store.state.delivery.deliverys
  }

   created(){
    this.$store.dispatch('delivery/get_deliverys')
    this.$store.dispatch('providers/get_providers')
  }
};
</script>

<style scoped>

</style>
