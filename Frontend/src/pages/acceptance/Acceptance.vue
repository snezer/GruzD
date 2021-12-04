<template>
  <div>
    <AcceptanceGalleria :photos-product="photos" />
    <div class="p-grid">
      <div class="p-col-12">
        Тип события: {{eventTypeWord[event.ClassifiedType]}}
      </div>
      <div class="p-col-12">
        Дата возникновения события: {{calcDate(event.Occured)}}
      </div>
      <div class="p-col-12">
        Событие обработано: {{event.Process?'Да':'Нет'}}
      </div>
      <div class="p-col-12">
        Дата обработки события: {{calcDate(event.ProcessTime)}}
      </div>
      <div class="p-col-12">
      Вес: {{event.Weight}} тонн
      </div>
    </div>
    <div class="p-grid" style="margin-top: 10px">
      <div class="p-col-6 p-text-center">
        <Button class="p-button-outlined p-button-danger p-button-lg" label="Ошибка"></Button>
      </div>
    </div>
  </div>
</template>

<script>
import { Component, Vue} from "vue-property-decorator";
import AcceptanceGalleria from "@/components/acceptance/AcceptanceGalleria";
import { EventTypeWord } from "@/models/enum/EventType";
@Component({
  components: { AcceptanceGalleria },
  props:{
    event: {
      type: Object,
      required: true,
    }
  }
})
export default class Acceptance extends Vue {
  eventTypeWord = EventTypeWord
  calcDate(date){
    if (!date) return '-'
    return new Date(date).toLocaleDateString('ru-Ru', {year: "numeric", month: "2-digit", day: "2-digit", hour: "2-digit", minute: "2-digit", second: "2-digit"})
  }
  get photos() {
    try{
      return this?.event.Base64PicsIds.map( photo=>{
        return {
          itemImageSrc: `/api/v1/File/${photo}`,
          thumbnailImageSrc: `/api/v1/File/${photo}`,
          alt: "Description for Image 1",
          title: "Title 1"
        }
      })
    }catch (e) {
      return [
        {
          itemImageSrc: 'https://s0.rbk.ru/v6_top_pics/ampresize/media/img/9/44/755283020588449.jpg',
          thumbnailImageSrc: 'https://s0.rbk.ru/v6_top_pics/ampresize/media/img/9/44/755283020588449.jpg',
          alt: "Описание события",
          title: 'Заголовок'
        }
      ]
    }

  }
};
</script>

<style>
.p-datatable .p-datatable-thead > tr > th {
  background-color: #372235;
  color: white;
}
</style>
