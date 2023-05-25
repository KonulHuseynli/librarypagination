var click = document.querySelectorAll('.link');

click.forEach(element => {element.addEventListener('click',function(){
    modal.style.display="block"
    main.style.display="none"
})})
document.querySelectorAll('.iks').forEach(item=>{item.addEventListener('click',function(){
    modal.style.display="none"
    main.style.display="block"
})})

var swiper = new Swiper(".mySwiper",{
    slidesPerView: 4,
    spaceBetween: 5,
    freeMode: true,
    pagination: {
      el: ".swiper-pagination",
      clickable: true,
    },
})