window.auth = {
    postJson: async function (url, data) {
        const res = await fetch(url, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            credentials: 'same-origin',
            body: JSON.stringify(data)
        });

        const text = await res.text();
        return { status: res.status, body: text };
    }
};
