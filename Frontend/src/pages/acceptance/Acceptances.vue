<template>
  <div>
    <div class="p-grid">
      <div class="p-col-9">
        <div class="p-grid">
          <div class="p-col-12 caption">
            В разгрузке
          </div>
        </div>
        <DataTable
          :value="acceptances"
          :selection.sync="selectedAcceptance"
          selectionMode="single"
          dataKey="id"
          responsiveLayout="scroll"
          rowGroupMode="subheader"
          groupRowsBy="supplier.name"
          :expandableRowGroups="true"
          :expandedRowGroups.sync="expandedRowGroups"
          sortMode="single"
          sortField="supplier.name"
          :sortOrder="1"
          class="gruzd"
        >
          <Column field="supplier.name" header="Поставщик"></Column>
          <Column field="date" header="Дата приемки">
            <template #body="slotProps">
              <span>{{slotProps.data.date.toLocaleDateString('ru-Ru', {year: 'numeric', month: '2-digit', day: "numeric", hour: "numeric", minute: "numeric"})}}</span>
            </template>
          </Column>
          <Column field="product.name" header="Продукт"></Column>
          <Column field="status" header="Статус"></Column>
          <Column field="defective" header="Брак">
            <template #body="slotProps">
              <span v-if="slotProps.data.defective">{{slotProps.data.defective}} брака</span>
              <span v-else>-</span>
            </template>
          </Column>
          <Column field="dischargeZone" header="Зона разгрузки"></Column>
          <Column field="numberTrainCar" header="Номер вагона"></Column>
          <template #groupheader="slotProps">
            <span>{{slotProps.data.supplier.name}}</span>
          </template>
        </DataTable>
      </div>
      <div class="p-col-3" style="background-color: #372235 ">
        <Acceptance v-bind:acceptance="selectAcceptance"/>
      </div>
    </div>
    <div class="p-grid" style="margin-top: 50px">
      <div class="p-col-12 caption">
        На проверке
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import {Component, Vue} from 'vue-property-decorator';
import Acceptance from '@/pages/acceptance/Acceptance.vue';
@Component({
  components: { Acceptance },
})
export default class Acceptances extends Vue {
  components: { Acceptance };
  selectedAcceptance:Acceptance | null = null;
  expandedRowGroups = null;

  get selectAcceptance() {
    return JSON.parse(JSON.stringify(this.selectedAcceptance));
  }
  get acceptances() {
    return this.$store.state.acceptance.acceptances;
  }

  created(){
    this.selectedAcceptance = this.acceptances[0]
  }
}
</script>

<style>
.p-datatable .p-datatable-tbody > tr{
  background: unset;
  color: unset;
  outline: unset;
}
.p-datatable.p-datatable-hoverable-rows .p-datatable-tbody > tr:not(.p-highlight):hover{
  background: #372235;
  color: unset;
}
.p-datatable .p-datatable-tbody > tr.p-highlight{
  background: #372235;
  color: unset;
}
.p-datatable .p-datatable-tbody > tr > td{
  border: unset
}
.gruzd thead{
  display: none;
}
.p-rowgroup-header{
  border-bottom: 1px #322F43 solid;

}
.caption{
  height:41px;
  background-color: rgba(255,255,255,.1)
}
</style>
