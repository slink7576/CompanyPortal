
$(document).ready(function () {
    
    var dates = $(".date");
    var i;
    for (i = 0; i < dates.length; i++)
    {
      
        var parseDate = dates[i].innerText.split(' ')[0].split('.')[2]
            + '-' + dates[i].innerText.split(' ')[0].split('.')[1]
            + '-' + dates[i].innerText.split(' ')[0].split('.')[0]
            + ' ' + dates[i].innerText.split(' ')[1] 
        var createdDate = moment(parseDate).fromNow();
        dates[i].innerText = createdDate;
    }
   
        
}
);
