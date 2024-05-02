function SortByPublisher() {
    var url = '/Home/GetBooksSortedByPublisher';
    var type = 'GET';
    $('#bookhead').html('Books Sorted by Publisher');
    AjaxCall(url, type, null);
}

function SortByAuthor() {
    var url = '/home/GetBooksSortedByAuthor';
    var type = 'GET';
    $('#bookhead').html('Books Sorted by Author');
    AjaxCall(url, type, null);
}

function SortByPublisherSP() {
    var url = '/home/GetBooksSortedByPublisherSP';
    var type = 'GET';
    $('#bookhead').html('Books Sorted by Publisher through stored procedure.');
    AjaxCall(url, type, null);
}

function SortByAuthorSP() {
    var url = '/home/GetBooksSortedByAuthorSP';
    var type = 'GET';
    $('#bookhead').html('Books Sorted by Author through Stored Procedure.');
    AjaxCall(url, type, null);
}
function GetTotalPrice() {
    var url = '/home/GetTotalPrice';
    var type = 'GET';
    AjaxCall(url, type, null);
}
function SaveList() {
    var url = '/home/BulkInsertBooks';
    var type = 'POST';
    var data = [];
    $('#bookTable tbody tr').each(function () {
        var book = {
            Title: $(this).find('td:eq(0)').text(),
            AuthorFirstName: $(this).find('td:eq(1)').text().split(' ')[0],
            AuthorLastName: $(this).find('td:eq(1)').text().split(' ')[1],
            Publisher: $(this).find('td:eq(2)').text(),
            Price: parseFloat($(this).find('td:eq(3)').text()),
            PublicationYear: parseInt($(this).find('td:eq(4)').text()),
            CityOfPublication: $(this).find('td:eq(5)').text(),
            Medium: $(this).find('td:eq(6)').text(),
            MLACitation: $(this).find('td:eq(7)').text(),
            ChicagoCitation: $(this).find('td:eq(8)').text()
        };
        data.push(book);
    });
    AjaxCall(url, type, data);
}

function AjaxCall(Url, type, data) {

    $.ajax({
        url: Url,
        type: type,
        data: { book: JSON.stringify(data) },
        dataType: 'json',
        success: function (response) {
            if (Array.isArray(response)) {
                if (response.length > 0) {
                    response.forEach(function (element, index) {
                        var row = `<tr><td>${element.title}</td><td>${element.authorFirstName} ${element.authorLastName}</td><td>${element.publisher}</td><td>${element.price}</td><td>${element.publicationYear}</td><td>${element.cityOfPublication}</td><td>${element.medium}</td><td>${element.mlaCitation}</td><td>${element.chicagoCitation}</td></tr>`;
                        if (index == 0) {
                            $('#bookTable tbody').html(row);
                        }
                        else {
                            $('#bookTable tbody').append(row);
                        }

                    });
                }
                else {
                    alert('An error occurred: No record to display');
                }
            } else {
                console.error('Response is not an array.');
                if (response.value) {
                    alert('Success : '+ response.value);
                } else {
                    alert('An error occurred: ' + xhr.responseText);
                }
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            alert('An error occurred: ' + xhr.responseText);
        }
    });
}