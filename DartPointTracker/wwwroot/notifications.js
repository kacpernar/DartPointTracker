window.requestNotificationPermission = () => {
    if ('Notification' in window) {
        if (Notification.permission === 'default') {
            Notification.requestPermission().then(permission => {
                if (permission === 'granted') {
                    console.log('Notifications enabled.');
                } else if (permission === 'denied') {
                    console.log('Notifications denied.');}});}
    } else {
        console.log('This browser does not support notifications.');}};

