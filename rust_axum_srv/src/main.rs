use axum::{
    routing::get,
    Router,
    response::Json,
};
use std::sync::Arc;
use serde_json::{json};
use serde::Serialize;

#[derive(Serialize)]
struct Bars {
    data: Vec<Bar>,
}


#[derive(Clone, Copy, Serialize)]
struct Bar {
    open: f32,
    high: f32,
    low: f32,
    close: f32,
}

struct AppSate {
    bars: Bars,
}

#[tokio::main]
async fn main() {
    let bars = Bars {
        data: vec![Bar {
            open: 432.3,
            high: 234.3,
            low: 324.3,
            close: 23.,
        }; 100],
    };

    let shared_state = Arc::new(AppSate { bars: bars });

    // build our application with a single route
    let app = Router::new()
        .route("/", get(|| async { "Hello, World!" }))
        .route("/bars", get({
            let s = Arc::clone(&shared_state);
            //let bars = (*ss).clone();
            Json(json!(s.bars))
        }));

    // run our app with hyper, listening globally on port 3000
    let listener = tokio::net::TcpListener::bind("0.0.0.0:3000").await.unwrap();
    axum::serve(listener, app).await.unwrap();
}