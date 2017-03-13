function showDetails(id, element) {
    $('#news_' + id).text($('#newsHidden_' + id).text());
    $(element).css('display', 'none');
};