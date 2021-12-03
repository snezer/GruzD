<template>
  <div>
    <div class="acceptance-wrapper">
      <div class="acceptance-control">
        <Control :visible-control="false"/>
      </div>
      <div class="acceptance-events">
        <div class="p-grid">
          <div class="p-col-12">
            <DataTable :value="acceptances" :selection.sync="selectedAcceptance" selectionMode="single" dataKey="id">
              <Column field="code" header="Тип события"></Column>
              <Column field="date" header="Время события">
                <template #body="slotProps">
                  {{slotProps.data.date.toLocaleDateString('ru-Ru', {year: "numeric", month: "2-digit", day: "2-digit", hour: "2-digit", minute: "2-digit", second: "2-digit"})}}
                </template>
              </Column>
              <Column field="category" header="Описание"></Column>
            </DataTable>
          </div>
          <div class="p-col-12">
            <Acceptance :acceptance="selectAcceptance"/>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import {Component, Vue} from 'vue-property-decorator';
import Acceptance from '@/pages/acceptance/Acceptance.vue';
import Control from "@/pages/conrol/Control.vue";
@Component({
  components: { Control, Acceptance },
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
.acceptance-wrapper{
  display: grid;
  grid-template-columns: 7fr 5fr;
}
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
