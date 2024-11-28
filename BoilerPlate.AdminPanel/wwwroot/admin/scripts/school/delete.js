$('.btnDelete').click(function (e) {
    e.preventDefault()
    const id = $(this).data('id')
    deleteRequest({
        id: id,
        url: getPath('/schools/delete/'),
        href: getPath('/schools')
    })
})