$('.btnDelete').click(function (e) {
    e.preventDefault()
    const id = $(this).data('id')
    deleteRequest({
        id: id,
        url: '/approle/delete/',
        href: '/approle'
    })
})